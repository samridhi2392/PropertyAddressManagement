using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PropertyAddressManagement.Startup))]
namespace PropertyAddressManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
