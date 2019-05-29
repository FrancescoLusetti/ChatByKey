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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet.AspNetCore;
using MQTTnet.Server;

namespace ApiBroker
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //Opzioni del server MQTT
            var option = new MqttServerOptionsBuilder()

                .WithDefaultEndpointPort(1883)//Dove mandare i messaggi
                .WithEncryptedEndpointPort(1883) //Dove mandare i messaggi con criptazione TLS 1.2
                .WithEncryptionSslProtocol(System.Security.Authentication.SslProtocols.Tls12)
                //.WithStorage(new RetainedMessageHandler()) //salva i messaggi in un file JSON
                                                           //.WithApplicationMessageInterceptor() Mi serve per intercettare dei pacchetti
                                                           //.WithSubscriptionInterceptor() serve ad intercettare delle scuiaocvbnsivottoscrizioni a dei determinati canali
                                                           //.WithConnectionValidator() serve a mettere una password alla connessione
                                                           //.WithDefaultCommunicationTimeout() serve a cambiare il tempo di timeout
                .WithConnectionBacklog(100)//quante persone si possono collegare contemporaneamente
                                           //.WithPersistentSessions()//non so cosa faccia ma credo sia utile
                .Build();


            //Crea il seever MQTT
            services.AddHostedMqttServer(option);

            //Gestisce le connessioni
            services.AddMqttConnectionHandler();
            //gestisce le connessioni in WebSocket
            services.AddMqttWebSocketServerAdapter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
