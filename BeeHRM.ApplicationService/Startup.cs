using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Mvc;


[assembly: OwinStartupAttribute(typeof(BeeHrmClientWeb.Startup))]
namespace BeeHrmClientWeb
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();

            ConfigureOAuthTokenGeneration(app);

            ConfigureOAuthTokenConsumption(app);

            ConfigureWebApi(httpConfig);
         
            ///Purpose:Used To Enable CORS for API,Any API Controller Deriving From ApiController
           app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(httpConfig);

            AreaRegistration.RegisterAllAreas();
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
           // // Configure the db context and user manager to use a single instance per request
           // app.CreatePerOwinContext(ApplicationDbContext.Create);
           // app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
           // app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

           //// var issuer = WebConfiguration.ReadAppSetting("serverAddress");

           // OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
           // {
           //     //For Dev enviroment only (on production should be AllowInsecureHttp = false)
           //     AllowInsecureHttp = true,
           //     TokenEndpointPath = new PathString("/oauth/token"),
           //     AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
           //     Provider = new CustomOAuthProvider(),
           //    AccessTokenFormat = new CustomJwtFormat(issuer)
           // };

           // // OAuth 2.0 Bearer Access Token Generation
           // app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            //var issuer = WebConfiguration.ReadAppSetting("serverAddress");
            //string audienceId = ConfigurationManager.AppSettings["authClient"];
            //byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["authSecret"]);

            //// Api controllers with an [Authorize] attribute will be validated with JWT
            //app.UseJwtBearerAuthentication(
            //    new JwtBearerAuthenticationOptions
            //    {
            //        AuthenticationMode = AuthenticationMode.Active,
            //        AllowedAudiences = new[] { audienceId },
            //        IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
            //        {
            //            new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
            //        }
            //    });
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            //config.Filters.Add(new ValidateViewModelAttribute());

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
