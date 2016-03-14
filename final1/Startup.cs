using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(final1.Startup))]
namespace final1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
