using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Infrastructure.EntityFrameworkCore;
//using Project.Identity.Entity;
//using Project.Infrastructure.EntityFrameworkCore;

namespace Project.AdminWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // �˷���������ʱ���á�ʹ�ô˷�����������ӵ�������
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddDbContext<EFContext>(options => options.UseSqlServer("Server=.;Database=Project;User=sa;Password=123456;"));
            services.AddDbContext<EFContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings")));
           // services.AddIdentity<Domain.Entity.User, IdentityRole>().AddEntityFrameworkStores<UserDBContext>()

            //Password Strength Setting
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //Setting the Account Login page
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here,
                                                      // ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here,
                                                        // ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is
                                                                    // not set here, ASP.NET Core
                                                                    // will default to
                                                                    // /Account/AccessDenied
                options.SlidingExpiration = true;

            });
        }

        // �˷���������ʱ���á�ʹ�ô˷�������http����ܵ���
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
                            //��¼�쳣��־
                            var path = $"d:\\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
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
                // Ĭ��hstsֵΪ30�졣��������ҪΪ�����������Ĵ����ã������https://aka.ms/aspnetcore-hsts��
                app.UseHsts();
            }
            app.UseHttpsRedirection();//httpǿ����תhttps
            app.UseStaticFiles();//��̬��Դ
            app.UseRouting();//ע��·��
            app.UseAuthorization();//�����֤��������UseEndpoints֮ǰ
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });//ע��˵� Ĭ��·�ɱ�
        }

    }
}
