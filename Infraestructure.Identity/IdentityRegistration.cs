﻿using Core.Application.Dtos.Generic;
using Core.Application.Entities;
using Core.Application.Helpers.Mail;
using Infraestructure.Identity.Context;
using Infraestructure.Identity.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Identity
{
    public static class IdentityRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseMySql(Environment.GetEnvironmentVariable("IdentityConnection") ?? configuration.GetConnectionString("IdentityConnection"), new MySqlServerVersion(new Version(10, 6, 16)),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName).SchemaBehavior(MySqlSchemaBehavior.Ignore));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/Account";
                option.AccessDeniedPath = "/User/AccessDenied";
            });
            if (Environment.GetEnvironmentVariable("SmtpPassword") != null)
            {
                _ = services.Configure<MailSettings>(x =>
                {
                    x.EmailFrom = Environment.GetEnvironmentVariable("EmailFrom");
                    x.SmtpHost = Environment.GetEnvironmentVariable("SmtpHost");
                    x.SmtpPort = int.Parse(Environment.GetEnvironmentVariable("SmtpPort"));
                    x.DisplayName = Environment.GetEnvironmentVariable("DisplayName");
                    x.SmtpUser = Environment.GetEnvironmentVariable("SmtpUser");
                    x.SmtpPassword = Environment.GetEnvironmentVariable("SmtpPassword");
                });
            }
            else
            {
                services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            }

            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; //patrue
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new GenericApiResponse<string>
                        {
                            Message = "You're Not Authorized",
                            Success = false,
                            Statuscode = 401
                        });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 404;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new GenericApiResponse<string>
                        {
                            Message = "You're Not Authorized to access to this resource",
                            Success = false,
                            Statuscode = 404
                        });
                        return c.Response.WriteAsync(result);
                    }

                };
            });
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MailSettings>>().Value);
        }

        public static async Task AddIdentityRolesAsync(this IServiceProvider services)
        {
            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await DefaultRoles.Seed(userManager, roleManager);
                await DefaultOwner.Seed(userManager, roleManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
