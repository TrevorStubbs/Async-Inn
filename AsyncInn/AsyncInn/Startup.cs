using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AsyncInn
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // if we want to lock down the whole site
            // options => options.Filters.Add(new AuthorizeFilter))
            services.AddControllers(options =>
            {
                // Make all controllers locked
                options.Filters.Add(new AuthorizeFilter());
            })
                // Prevent self references in the models
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            // Allow entity framework to use a DbContext
            services.AddDbContext<AsyncInnDbContext>(options =>
           {
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
           });

            // Initialze Idenity in the app
            // ================================ THIS ORDER MATTERS =============================================
            // This has to go above the .AddAuthentication
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AsyncInnDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                // Must define the JWT Bearer defaults
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Define the JwtBearer's parameters
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWTIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTKey"]))
                };
            });

            // add my policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("TopLevelPrivileges", policy => policy.RequireRole(ApplicationRoles.DistrictManager));
                options.AddPolicy("ElevatedPrivileges", policy => policy.RequireRole(ApplicationRoles.DistrictManager, ApplicationRoles.PropertyManager));
                options.AddPolicy("BottomLevelPrivileges", policy => policy.RequireRole(ApplicationRoles.DistrictManager,ApplicationRoles.PropertyManager, ApplicationRoles.Agent)); 
                //can also use policy.RequireClaim("FavoriteColor");
            });

            services.AddTransient<IHotel, HotelRepository>();
            services.AddTransient<IRoom, RoomRepository>();
            services.AddTransient<IAmenities, AmenitiesRepository>();
            services.AddTransient<IHotelRoom, HotelRoomRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ======================= THIS ORDER MATTERS =======================================
            app.UseRouting();

            // Authentication before Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Get the services from the UserManager
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Initialize the roles using the seed data.
            RoleInitializer.SeedData(serviceProvider, userManager, Configuration); // Do something herre

            app.UseEndpoints(endpoints =>
            {
                // Define our own endpoints
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
