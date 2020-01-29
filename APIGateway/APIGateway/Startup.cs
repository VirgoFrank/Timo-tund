using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProxyKit;

namespace APIGateway
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
            services.AddSingleton<ServerPool>();
            services.AddProxy();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ServerPool servers)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

           
            app.UseMvc();

    
           
            app.RunProxy(
                async context =>
                {
                    if(!servers.IsEmpty())
                    {
                        var host = servers.Next();

                        var response= await context
                            .ForwardTo(host)
                            .Send();

                        while(!response.IsSuccessStatusCode && host!= null)
                        {
                            servers.Remove(host);
                            host = servers.Next();
                            if(host != null)
                            {
                                response = await context
                                .ForwardTo(host)
                                .Send();
                            }
                            return response;
                        }
                    }
                    return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
                });

         
        }
    }
}
