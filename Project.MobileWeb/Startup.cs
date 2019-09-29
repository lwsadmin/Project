using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Project.MobileWeb
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
            //services.AddDbContext<EFContext>(options => options.UseSqlServer("Server=.;Database=Project;User=sa;Password=123456;"));
            //设置依赖注入
            SetDepend("Project.Application", services);
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
                // 默认hsts值为30天。您可能需要为生产场景更改此设置，请参阅https://aka.ms/aspnetcore-hsts。
                app.UseHsts();
            }
            app.UseHttpsRedirection();//http强制跳转https
            app.UseStaticFiles();//静态资源
            app.UseRouting();//注入路由
            app.UseAuthorization();//身份认证，必须在UseEndpoints之前
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
