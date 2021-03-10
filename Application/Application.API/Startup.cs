using Application.API.IOC;
using Application.API.Segurança;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Web.Configuration;
using System.Web.Http;

[assembly: OwinStartup(typeof(Application.API.Startup))]

namespace Application.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureWebApi(config);
            ConfigureOAuth(app);

            //Permite requisições de qualquer URL (Domínio)
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //Informa que está usando Web API
            app.UseWebApi(config);

        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            var formatters = config.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;


            //Configura JSON como formato de transmissão de dados principal
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //Remove XML das Configurações
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //Configura o JSON para retornar sempre identado
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //Configura o JSON para retonnar sempre com o padrão Camel Case
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //Salva as Configurações
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.DependencyResolver = new UnityResolver(new ConfigureIoC().Register());
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var time = Convert.ToDouble(WebConfigurationManager.AppSettings["tokenExpiracao"]);

            //Configura as opcoes o servidor de Autorizacao do OAuth
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true, //Desabilita HTTPS
                TokenEndpointPath = new PathString("/api/security/token"), //Especifica onde fazer a requisicao para obter um token
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(time),//Quanto tempo o token vai se manter ativo
                Provider = new AuthorizationServerProvider() // Quem vai ser responsavel por autenticar o serviço
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


        }
    }
}
