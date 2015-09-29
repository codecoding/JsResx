using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JsResx.Example.Startup))]
namespace JsResx.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
