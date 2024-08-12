using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ///allows anyone to send a jwt token if true to this server
                    ///if it was false only the token created on this server will be allowed to make requests with the server
                    ValidateIssuerSigningKey = true,

                    //used for decoding the signature of the jwt token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),

                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
            });

            return services;
        }
    }
}