using Microsoft.AspNetCore.Builder;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.Models;


namespace Todo.Api
{
    public class Startup
    {

        //readonly string MyStrictCorsPolicy = "MyStrictCorsPolicy";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            IServiceCollection serviceCollections = services.AddDbContext<zedContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("zed"));
            });
            Debug.WriteLine(string.Format("count: {0}", serviceCollections.Count));

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyStrictCorsPolicy,
            //                      builder =>
            //                      {
            //                          builder.WithOrigins("https://a.com",
            //                                              "https://b.org")
            //                                .AllowAnyHeader()
            //                                .AllowAnyMethod();
            //                      });
            //});


            services.AddCors(o => o.AddPolicy("MyOpenDoorPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState);
                    var result = new BadRequestObjectResult(problemDetails);
                    result.ContentTypes.Add("application/json");
                    return result;
                };
            });

            // in dev mode, keep the following line commented off
            // services.AddResponseCaching();
            services.AddControllers();
        }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors(MyStrictCorsPolicy);
            app.UseCors("MyOpenDoorPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "api/{controller=todos}/{action=getdefault}");
            });
        }
    }
}
