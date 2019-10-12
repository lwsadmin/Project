using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Entity;
//using Domain.Entity;
//using Fluent.Infrastructure.FluentModel;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Infrastructure.EntityFrameworkCore;
using Project.Infrastructure.Identity;

namespace Project.AdminWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 此方法由运行时调用。使用此方法将服务添加到容器。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings"]));
            //services.AddDbContext<EFContext>(options => options.UseSqlServer(Configuration["ConnectionStrings"]));
            services.AddIdentity<ProjectUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            SetDepend("Project.Application", services);
            // services.AddIdentity<ProjectUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            //Password Strength Setting
            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = true;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequiredUniqueChars = 6;

            //    // Lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Lockout.MaxFailedAccessAttempts = 10;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings
            //    options.User.RequireUniqueEmail = true;
            //});

            //Setting the Account Login page
            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    options.LoginPath = "/Account/Login"; // If the LoginPath is not set here,
            //                                          // ASP.NET Core will default to /Account/Login
            //    options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here,
            //                                            // ASP.NET Core will default to /Account/Logout
            //    options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is
            //                                                        // not set here, ASP.NET Core
            //                                                        // will default to
            //                                                        // /Account/AccessDenied
            //    options.SlidingExpiration = true;

            //});
        }

        // 此方法由运行时调用。使用此方法配置http请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(c =>
                {
                    c.Run(async d =>
                    {
                        var ex = d.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            //记录异常日志
                            var path = $"c:\\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true);
                            sw.WriteLine(ex.Error.Message);
                            sw.WriteLine(ex.Error.StackTrace);
                            sw.Flush();
                            sw.Close();
                        }
                        await d.Response.WriteAsync(ex?.Error?.Message ?? "an error occure");
                    });
                });
                //  app.UseExceptionHandler("/Home/Error");
                // 默认hsts值为30天。您可能需要为生产场景更改此设置，请参阅https://aka.ms/aspnetcore-hsts。
                app.UseHsts();
            }
            app.UseHttpsRedirection();//http强制跳转https
            app.UseStaticFiles();//静态资源
            app.UseRouting();//注入路由
            app.UseAuthorization();//身份认证，必须在UseEndpoints之前
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });//注入端点 默认路由表
        }

        private void SetDepend(string assemblyName, IServiceCollection services)
        {
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> classList = assembly.GetTypes().Where(c => c.IsClass).ToList();
                foreach (var item in classList)
                {
                    var interfaceTypeArray = item.GetInterfaces();
                    if (interfaceTypeArray.Length > 0)
                    {
                        services.AddTransient(interfaceTypeArray[0], item);
                    }
                }
            }
        }
    }
}
