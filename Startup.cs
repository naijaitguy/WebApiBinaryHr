using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiBinaryHr.Entities;
using WebApiBinaryHr.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApiBinaryHr._Helper;
using Microsoft.OpenApi.Models;

namespace WebApiBinaryHr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private static void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            const string adminRoleName = "Director";
            string[] roleNames = { adminRoleName, "Supervirsor", "User", "Admin" };

            foreach (string roleName in roleNames)
            {
                CreateRole(serviceProvider, roleName);
            }

            // Get these value from "appsettings.json" file.
            string adminUserEmail = "test@binaryhr.com";
            string adminPwd = "MYpassword01@#";
            AddUserToRole(serviceProvider, adminUserEmail, adminPwd, adminRoleName);
        }

        /// <summary>
        /// Create a role if not exists.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="roleName">Role Name</param>
        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }

        /// <summary>
        /// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="userEmail">User Email</param>
        /// <param name="userPwd">User Password. Used to create the user if not exists.</param>
        /// <param name="roleName">Role Name</param>
        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail,string userPwd, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            Task<User> checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();

            User appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                User newAppUser = new User
                {
                    Email = userEmail,
                    UserName = userEmail
                };

                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    appUser = newAppUser;
                }
            }

            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
            newUserRole.Wait();
        }


        // This method gets called by the runtime. Use this method to add services to the container.

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            ////////////////////////////////////register database service-----------------------------------
            services.AddDbContext<BinaryHrDbContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("BinaryHr")));
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<BinaryHrDbContext>();
            services.AddScoped<IUnitofwork, Unitofwork>();
            services.AddScoped<IProfileServices, ProfileServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IApplicationServices, ApplicationServices>();
            services.AddScoped<IExperienceServices, ExperienceServices>();
            services.AddScoped<IAcademicServices, AcademicServices>();
            services.AddScoped<IJobServices, JobServices>();

            //////////////////////////Enable  Cross ORigin//////////////////////////////////////////

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
               builder => builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               );
            });

            //////////////////////////json seriliaze/////////////////////////////////
            services.AddControllers().AddJsonOptions(opt => {
                opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                opt.JsonSerializerOptions.DictionaryKeyPolicy = null;
                opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            }
            );
            //////////////////////Add Swagger-------------------/////////////////////////
            ///

            services.AddSwaggerGen(c =>
            { c.SwaggerDoc("v1", new OpenApiInfo { Version="v1", Title="BinaryHrwebApi", Description="Api for Angular Back End" }); });
            

            

            ///

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(cfg =>
              {
                  cfg.RequireHttpsMetadata = false;
                  cfg.SaveToken = true;
                  cfg.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidIssuer = JwtokenOptions.Issuer,
                      ValidAudience = JwtokenOptions.Issuer,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtokenOptions.Key)),
                      ClockSkew = TimeSpan.Zero // remove delay of token when expire
                  };
              });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
             app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
             app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "BinaryHrWebApi"); });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
          

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapRazorPages();
            });

           CreateRolesAndAdminUser(serviceProvider);
        }
    }
}
