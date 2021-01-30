using FluentValidation;
using FluentValidation.AspNetCore;
using KanIBAN.API.Config;
using KanIBAN.API.Data.Request;
using KanIBAN.API.Data.Request.Validators;
using KanIBAN.API.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KanIBAN.API
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
            services.AddControllers()
                .AddFluentValidation()
                .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.WriteIndented = false;
                    });
            services.AddTransient<IBANResponseBuilder>();
            services.AddScoped<IBANFormatProvider>(_ => new IBANFormatProvider(IBANCountryFormats.IBANFormats));
            services.AddTransient<IValidator<ValidateIBANRequest>, ValidateIBANRequestValidator>();
            services.AddTransient<IValidator<ValidateIBANListRequest>, ValidateIBANListRequestValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}