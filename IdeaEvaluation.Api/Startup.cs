using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdeaEvaluation.DataAccess.Models;
using IdeaEvaluation.DataAccess.Repository;
using IdeaEvaluation.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace IdeaEvaluation.Api
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
            services.AddEntityFrameworkSqlite().AddDbContext<IdeaEvaluationDBContext>();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson().AddControllersAsServices();
            services.AddCors();
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IIdeaEvaluationService,IdeaEvaluationService>();
            
         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           app.UseCors( options => 
           options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod() );
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseMvc();
        }
    }
}
