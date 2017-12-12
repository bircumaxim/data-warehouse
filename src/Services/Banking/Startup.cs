using Banking.Data;
using Banking.Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;


namespace Banking
{
    public class Startup
    {
        private readonly Info _info;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _info = new Info {Title = "Banking API", Version = "v1"};
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => { options.RespectBrowserAcceptHeader = true; })
                .AddXmlSerializerFormatters();
            services.AddDbContext<DbApplicationContext>(
                options => options.UseNpgsql(Configuration.GetConnectionString("Default")), ServiceLifetime.Singleton);
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(c => { c.SwaggerDoc(_info.Version, _info); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{_info.Version}/swagger.json", $"{_info.Title} - {_info.Version}");
            });

            app.UseMvc();
        }
    }
}