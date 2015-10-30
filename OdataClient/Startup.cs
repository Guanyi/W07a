using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OdataClient.Startup))]
namespace OdataClient
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
