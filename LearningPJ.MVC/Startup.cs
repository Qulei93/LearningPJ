using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearningPJ.MVC.Startup))]
namespace LearningPJ.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
