using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BloodDonation.Startup))]
namespace BloodDonation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
