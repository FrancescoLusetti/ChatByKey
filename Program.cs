using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace ApiBroker
{
    public class Program
    {
        private static int GetPort()
        {
            if (Environment.GetEnvironmentVariable("PORT") != null)
                return Int32.Parse(Environment.GetEnvironmentVariable("PORT"));
            else
                return 1883;
        }
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //Crea server usando Kestrel 
                .UseKestrel(o =>
                {
                    //Sente i pacchetti MQTT da ogni IP sulla porta 1883
                    o.ListenAnyIP(GetPort(), l => l.UseMqtt());
                    //Sente i pacchetti HTTP su 5000
                    //o.ListenAnyIP(5000);
                })
                .UseStartup<Startup>();
    }
}
