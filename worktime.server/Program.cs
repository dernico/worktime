using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace worktime.server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try{
                var cert = new X509Certificate2("cert.pfx", "Test123!", X509KeyStorageFlags.UserKeySet);

                var host = new WebHostBuilder()
                    //.UseUrls("https://*:3000")
                    //.UseKestrel(cfg => cfg.UseHttps(cert))
                    .UseUrls("http://*:3000")
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();

                host.Run();
            
            }
            catch(Exception ex){
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
