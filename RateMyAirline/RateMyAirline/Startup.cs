using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RateMyAirline.Startup))]
namespace RateMyAirline
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
