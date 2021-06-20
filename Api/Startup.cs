using System;
using System.Reflection;
using BaseApi.Configuration;
using BaseApi.Controllers.Users;
using Microsoft.EntityFrameworkCore;

namespace BaseApi
{
    using Domain.Shared.Interfaces;
    using Domain.Users.Interfaces;
    using Domain.Users.Models;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Storage.Users;

    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var domainAssembly = AppDomain.CurrentDomain.Load("Domain");
            services.AddMediatR(domainAssembly);
            services.AddDependencyInjection();

            services.AddDbContext<UserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}