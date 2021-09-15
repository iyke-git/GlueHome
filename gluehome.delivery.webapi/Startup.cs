using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gluehome.delivery.repository;
using gluehome.delivery.security;
using gluehome.delivery.security.models;
using gluehome.delivery.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace gluehome.delivery.webapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var securityOptions = new ClientSecurityOptions();
            Configuration.GetSection(ClientSecurityOptions.Security)
                .Bind(securityOptions);
                
            services.AddControllers();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Convert.FromHexString(securityOptions.ClientKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5000",
                    ValidateLifetime = true
                };
            });
            services.AddScoped<IJwt, JwtTokenGenerator>();
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IPartnersRepository, PartnerRepository>();
            services.AddScoped<IRecipientRepository, RecipientRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "gluehome.delivery.webapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "gluehome.delivery.webapi v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
