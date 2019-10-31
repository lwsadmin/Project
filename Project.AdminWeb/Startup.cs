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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service;
using IService;
using Project.Domain.IUnitOfWork;
using Project.Infrastructure.UnitOfWork;
using SqlSugar;

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

            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            // services.AddTransient(typeof(IUserAppService), typeof(UserAppService));
            SetDepend("Service", services);
            services.AddScoped<SqlSugar.ISqlSugarClient>(o =>
            {
                return new SqlSugar.SqlSugarClient(new SqlSugar.ConnectionConfig()
                {
                    ConnectionString = Configuration["ConnectionStrings"],
                    DbType = DbType.SqlServer,
                    InitKeyType = InitKeyType.Attribute,//�����Զ�ȡ��������������Ϣ
                    IsAutoCloseConnection = true,//�����Զ��ͷ�ģʽ��EFԭ��һ��
                });
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
                    pattern: "{area=member}/{controller=member}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
    name: "default2",
    pattern: "{controller=home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
    name: "defaultWithArea",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
            });//ע��˵� Ĭ��·�ɱ�
        }

        private void SetDepend(string assemblyName, IServiceCollection services)
        {
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> classList = assembly.GetTypes().Where(c => c.IsClass).ToList();
                foreach (var item in classList)
                {
                    var interfaceTypeArray = item.GetInterfaces().Where(c => c.FullName.Contains("IService")).ToList();
                    if (interfaceTypeArray.Count > 0)
                    {
                        var inter = interfaceTypeArray[0];
                        services.AddTransient(inter, item);
                    }
                }
            }
        }
    }
}
