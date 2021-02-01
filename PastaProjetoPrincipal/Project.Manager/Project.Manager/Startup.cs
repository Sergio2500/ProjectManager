using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Project.Manager.Startup))]

namespace Project.Manager
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {

                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //Aqui serve para redirecionar o usuário para outra rota, caso ele não tenha conta
                LoginPath = new PathString("/Autenticacao/Login")

            });
        }
    }
}
