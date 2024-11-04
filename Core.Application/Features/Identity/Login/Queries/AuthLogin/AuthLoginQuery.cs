﻿using Core.Application.Dtos.Account.Login;
using Core.Application.Dtos.Generic;
using Core.Application.Entities;
using Core.Application.Helpers.Identity;
using Core.Application.Helpers.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Application.Features.Identity.Login.Queries.AuthLogin
{
    public class AuthLoginQuery : IRequest<GenericApiResponse<AuthenticationResponse>>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

    public class AuthLoginQueryHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        : IRequestHandler<AuthLoginQuery, GenericApiResponse<AuthenticationResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly JWTSettings _jwtSettings
            = new JWTSettings()
            {
                Audience = Environment.GetEnvironmentVariable("Audience") ?? configuration["JWTSettings:Audience"],
                Issuer = Environment.GetEnvironmentVariable("Issuer") ?? configuration["JWTSettings:Issuer"],
                Key = Environment.GetEnvironmentVariable("Key") ?? configuration["JWTSettings:Key"],
                DurationInMinutes = int.Parse(Environment.GetEnvironmentVariable("DurationInMinutes") ?? configuration["JWTSettings:DurationInMinutes"])
            };

        public async Task<GenericApiResponse<AuthenticationResponse>> Handle(AuthLoginQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<AuthenticationResponse>();
            var User = await _userManager.FindByNameAsync(request.UserName);
            if (User == null)
            {
                response.Success = false;
                response.Message = $"No Account Register with {request.UserName}";
                response.Statuscode = 402;
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(User.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.Success = false;
                response.Message = $"Invalid Password";
                response.Statuscode = 409;
                return response;
            }
            if (!User.EmailConfirmed)
            {
                response.Success = false;
                response.Message = $"Account not confirm for {request.UserName}";
                response.Statuscode = 400;
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(User);
            response.Payload = new AuthenticationResponse();
            response.Payload.Id = User.Id;
            response.Payload.Name = User.Name;
            response.Payload.LastName = User.LastName;
            response.Payload.Email = User.Email;
            response.Payload.IsVerified = User.EmailConfirmed;
            var roles = await _userManager.GetRolesAsync(User).ConfigureAwait(false);
            response.Payload.Roles = roles.ToList();
            response.Payload.IsVerified = User.EmailConfirmed;
            response.Payload.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Payload.RefreshToken = ExtraMethods.GenerateRefreshToken().Token;

            return response;
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signCredentials
            );
            return jwtSecurityToken;
        }
    }
}
