using account_event_sourcing.Domain;
using account_event_sourcing.Domain.Event;
using account_event_sourcing.Repository;
using account_event_sourcing.Service;
using account_event_sourcing.Service.EventHandler;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace account_event_sourcing
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

            var dbSetting= Configuration["ConnectionStrings:DefaultConnection"];
            services.AddControllers();
            services.AddTransient<AccountService, AccountService>();
            services.AddTransient<AccountEventService, AccountEventService>();
            services.AddTransient<DomainEventRepository, DomainEventRepository>();
            services.AddTransient<Repository<Account, Guid>, AccountRepository>();
            services.AddTransient<Repository<AccountAggergate, Guid>, AccountAggergateRepository>();
            services.AddTransient<Repository<DomainEvent, long>, DomainEventRepository>();
            services.AddTransient<Service.EventHandler<DomainEvent>, AccountEventHandler>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddDbContext<AccountDbContext>(
                options => options.UseSqlServer(@dbSetting));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "account_event_sourcing", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "account_event_sourcing v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
