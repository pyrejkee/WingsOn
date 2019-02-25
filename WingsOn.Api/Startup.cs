using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Bll.Implementations;
using WingsOn.Bll.Interfaces;
using WingsOn.Dal;
using WingsOn.Domain;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;

namespace WingsOn.Api
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
            services.AddSingleton<IRepository<Flight>, FlightRepository>();
            services.AddSingleton<IRepository<Person>, PersonRepository>();
            services.AddSingleton<IRepository<Booking>, BookingRepository>();
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "WingsOn API",
                    Description = "WingsOn API",
                    Contact = new Contact
                    {
                        Email = "kiryl_kiryanchykau@epam.com",
                        Name = "Kiryl Kiryanchykau"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(configs => configs.SwaggerEndpoint("/swagger/v1/swagger.json", "WingsOn Api"));
        }
    }
}
