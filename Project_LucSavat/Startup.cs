using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_LucSavat.Startup))]
namespace Project_LucSavat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
