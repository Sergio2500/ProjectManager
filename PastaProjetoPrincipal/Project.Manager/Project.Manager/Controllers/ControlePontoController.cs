using Project.Manager.DBProject;
using Project.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Manager.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ControlePontoController : Controller
    {
        // GET: ControlePonto
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IncluirControle()
        {
            var lista = ColaboradorProjetoDao.ListarNomes();

            ViewBag.ListaDeColaboradores = new SelectList(lista, "Codigo", "Nome");
            //ViewBag.ListaDeColaboradores = new SelectList(ColaboradorProjetoDao.ListarTarefas(), "Id", "Id");
            //ViewBag.ListaDeColaboradores = new SelectList(ColaboradoresDao.ListarColaboradores(), "Id", "Nome");
            //(ColaboradoresDao.ListarColaboradores(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        public ActionResult IncluirControle(HoraTrabalhada hora)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }                
                ControlePontoDao.IncluirPonto(hora);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }

        [HttpGet]
        public ActionResult ListarPontos()
        {            
            var ponto = ControlePontoDao.ListarPontos();            

            return View(ponto);
        }


        private ActionResult VerificarPontos(int id, string view)
        {
            try
            {                
                var ponto = ControlePontoDao.BuscarPontos(id);

                if (ponto == null)
                {
                    throw new Exception("Este tipo de Skill não consta no sistema");
                }

                return View(view, ponto);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }


        [HttpGet]
        public ActionResult AlterarPonto(int id)
        {            
            ViewBag.ListaDeColaboradores = new SelectList(ColaboradoresDao.ListarColaboradores(), "Id", "Nome");
            
            return VerificarPontos(id, "AlterarPonto");
        }

        [HttpPost]
        public ActionResult AlterarPonto(HoraTrabalhada ponto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                ControlePontoDao.AlterarPonto(ponto);

                return RedirectToAction("ListarPontos");

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult MostrarDetalhes(int id)
        {
            return VerificarPontos(id, "MostrarDetalhes");
        }

        [HttpGet]
        public ActionResult ExcluirPonto(int id)
        {
            return VerificarPontos(id, "ExcluirPonto");
        }

        [HttpPost]
        public ActionResult ExcluirPonto(HoraTrabalhada ponto)
        {
            try
            {
                //SkillDao.ExcluirSkill(skill);
                ControlePontoDao.ExcluirPonto(ponto);

                return RedirectToAction("ListarPontos");
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }




    }
}