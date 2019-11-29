using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using VrBooking.Core.ApplicationServices;
using VrBooking.Core.DomainServices;
using VrBooking.Core.Entity;
using VrBooking.Infrastructure;
using VrBooking.Infrastructure.Repositories;
using VrBooking.RestApi.WebApp.Helper;
using VrBooking.RestApi.WebApp.Helper.impl;
using VrBooking.RestApi.WebApp.Seeder;
using VrBooking.RestApi.WebApp.Seeder.Impl;

namespace VrBooking.RestApi.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            // Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "TodoApiClient",
                    ValidateIssuer = false,
                    //ValidIssuer = "TodoApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            if (Env.IsDevelopment())
            {
                services.AddDbContext<VrBookingContext>(opt => opt.UseSqlite("Data Source = VrBooking.db"));
            }
            else
            {
                services.AddDbContext<VrBookingContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }

            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();
            services.AddScoped<IRepository<UserInfo>, UserInfoRepository>();
            services.AddScoped<IRepository<BookingOrder>, BookingRepository>();
            //services.AddScoped<IRepository<LoginUser>, LoginUserRepository>();

            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IBookingOrderService, BookingOrderService>();
            services.AddScoped<ILoginUserService, LoginUserService>();

            services.AddCors();
            services.AddTransient<IDbSeeder, DbSeeder>();

            services.AddSingleton<IAuthenticationHelper>(new AuthenticationHelper(secretBytes));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
