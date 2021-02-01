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
    public class ContatosController : Controller
    {
        // GET: Contatos
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Cliente");
        }

        [HttpGet]
        public ActionResult IncluirContato()
        {
            //ViewBag.ListaDeColaboradores = new SelectList(ColaboradoresDao.ListarColaboradores(), "Id", "Nome");
            ViewBag.ListaDeClientes = new SelectList(ClientesDao.ListarClientes(), "Id", "RazaoSocial");

            return View();
        }


        [HttpPost]
        public ActionResult IncluirContato(Contato contato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                ContatosDao.IncluirContato(contato);
                return RedirectToAction("Index", "Cliente");

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }

        }

        public ActionResult ListarContatos()
        {
            var ponto = ContatosDao.ListarContatos();

            return View(ponto);

        }

        public ActionResult VerificarContatos(int id, string view)
        {

            try
            {
                //var ColaboradorProjeto = ColaboradorProjetoDao.BuscarTarefas(id);
                var contato = ContatosDao.BuscarContatos(id);
                if (contato == null)
                {
                    throw new Exception("Este contato não consta na listagem");
                }

                return View(view, contato);

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        [HttpGet]
        public ActionResult AlterarContatos(int id)
        {
            ViewBag.ListaDeClientes = new SelectList(ClientesDao.ListarClientes(), "Id", "RazaoSocial");
            return VerificarContatos(id, "AlterarContatos");
        }

        [HttpPost]
        public ActionResult AlterarContatos(Contato contato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                ContatosDao.AlterarContatos(contato);

                return RedirectToAction("ListarContatos");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
                
            }
        }

        public ActionResult MostrarDetalhes(int id)
        {
            return VerificarContatos(id, "MostrarDetalhes");
        }

        [HttpGet]
        public ActionResult ExcluirContatos(int id)
        {
            return VerificarContatos(id, "ExcluirContatos");
        }

        [HttpPost]
        public ActionResult ExcluirContatos(Contato contato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                ContatosDao.ExcluirContatos(contato);

                return RedirectToAction("ListarContatos");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
                
            }
        }


    }

}