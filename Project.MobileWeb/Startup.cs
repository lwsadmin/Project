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

        // �˷���������ʱ���á�ʹ�ô˷�����������ӵ�������
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //services.AddDbContext<EFContext>(options => options.UseSqlServer("Server=.;Database=Project;User=sa;Password=123456;"));
            //��������ע��
            SetDepend("Project.Application", services);
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
