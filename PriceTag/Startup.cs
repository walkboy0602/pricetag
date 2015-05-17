using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PriceTag.Startup))]
namespace PriceTag
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
