using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RegService
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
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/GetCode/{*computerInfo}",async (ctx)=>{
                     EncryptionHelper help = new EncryptionHelper(EncryptionKeyEnum.KeyB);
             string md5String = help.GetMD5String(ctx.Request.RouteValues["computerInfo"].ToString());
            
          string registInfo = help.EncryptString(md5String);
                    byte[] codes=System.Text.Encoding.Default.GetBytes(registInfo);
                    await  ctx.Response.Body.WriteAsync(codes,0,codes.Length);
                });
                   
                endpoints.MapControllers();
            });
        }
    }
}
