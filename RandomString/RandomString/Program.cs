using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace RandomString
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rnd = new Random();
            int port = rnd.Next(6000,6999);
            HttpClient client = new HttpClient();
            var content = new StringContent(port.ToString(),Encoding.UTF8,"application/json");
            client.PostAsync("https://localhost:44349/api/register", content);
            CreateWebHostBuilder(args,port).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args,int port) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://localhost:"+port)
                .UseStartup<Startup>();
    }
}
