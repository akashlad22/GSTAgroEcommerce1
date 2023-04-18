using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(GSTAgroEcommerce.App_Start.Startup1))]

namespace GSTAgroEcommerce.App_Start
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "737816805057-5ks6319c89hhsh7k5mffdq8qn3gq1l1p.apps.googleusercontent.com\r\n",
                ClientSecret = "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"
            });

        }
    }
}
