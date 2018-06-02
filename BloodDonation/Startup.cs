using Microsoft.Owin;
using Owin;
using System.Net;

[assembly: OwinStartupAttribute(typeof(BloodDonation.Startup))]
namespace BloodDonation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ConfigureAuth(app);
        }
    }
}
