using System;
using System.Threading.Tasks;
using Lifestyle.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Lifestyle.App_Start.Startup))]

namespace Lifestyle.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            // Дополнительные сведения о настройке приложения см. на странице https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
