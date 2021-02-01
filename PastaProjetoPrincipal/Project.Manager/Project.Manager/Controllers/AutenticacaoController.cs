using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Project.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Manager.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: Autenticacao
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public ActionResult CadastrarUsuario()
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var lista = roleManager.Roles.ToList();

            var listaRoles = new List<string>();
            foreach (var item in lista)
            {
                listaRoles.Add(item.Name);
            }

            ViewBag.Roles = new SelectList(listaRoles);

            return View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioView usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //dados de armazenamento do usuário
            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);

            //criar a identidade do usuário
            var usuarioInfo = new IdentityUser()
            {
                UserName = usuario.Email
            };

            //Com a identidade, criamos o usuário
            IdentityResult result = usuarioManager.Create(usuarioInfo, usuario.Senha);

            //Se o usuário for criado, o autenticamos abaixo
            if (result.Succeeded)
            {
                //Uma forma de definir o nível do usuário
                //Define um papel role para o usuário
                usuarioManager.AddToRole(usuarioInfo.Id, usuario.Nivel);

                var autentica = System.Web.HttpContext
                    .Current.GetOwinContext().Authentication;

                var identidade = usuarioManager.CreateIdentity(usuarioInfo, DefaultAuthenticationTypes.ApplicationCookie);

                autentica.SignIn(new AuthenticationProperties() { IsPersistent = false }, identidade);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MensagemErro = result.Errors.FirstOrDefault();
                return View("_Erro");
            }

        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView usuario, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var usuarioStore = new UserStore<IdentityUser>();
            var usuarioManager = new UserManager<IdentityUser>(usuarioStore);

            var usuarioInfo = usuarioManager.Find(usuario.Email, usuario.Senha);

            if (usuarioInfo != null)
            {
                var autentica = System.Web.HttpContext.Current.GetOwinContext().Authentication;

                var identidade = usuarioManager.CreateIdentity(usuarioInfo, DefaultAuthenticationTypes.ApplicationCookie);

                autentica.SignIn(new AuthenticationProperties() { IsPersistent = true }, identidade);

                if (ReturnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(ReturnUrl);
                }

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.MensagemErro = "Usuário ou senha inválidos";
                return View("_Erro");
            }

        }

        public ActionResult Logout()
        {

            var autentica = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            autentica.SignOut();
            return RedirectToAction("Index", "Home");

        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public ActionResult IncluirRole()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public ActionResult IncluirRole(string role)
        {
            try
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                var objRole = new IdentityRole();
                objRole.Name = role;
                roleManager.Create(objRole);

                ViewBag.Resposta = "Role" + role + "Incluida com sucesso";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }





    }
}