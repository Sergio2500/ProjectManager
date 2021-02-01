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
    public class ColaboradorProjetoController : Controller
    {
        // GET: ColaboradorProjeto
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Index", "Projetos");
        }

        [HttpGet]
        public ActionResult IncluirTarefa()
        {
            
            ViewBag.ListaDeColaboradores = new SelectList(ColaboradoresDao.ListarColaboradores(), "Id", "Nome");
            ViewBag.ListaDeProjetos = new SelectList(ProjetosDao.ListarProjetos(), "Id", "Descricao");            
            
            return View();
        }

        [HttpPost]
        public ActionResult IncluirTarefa(ColaboradorProjeto colaboradorProjeto)
        {
            try
            {                

                if (!ModelState.IsValid)
                {
                    return View();
                }
                
                ColaboradorProjetoDao.IncluirTarefa(colaboradorProjeto);
                return RedirectToAction("Index", "Projetos");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        //[HttpGet]
        public ActionResult ListarTarefas()
        {            
            var lista = ColaboradorProjetoDao.ListarTarefas();
            
            ViewBag.ListarProjetos = ColaboradorProjetoDao.ListarProjetosInfo();

            //return RedirectToAction("Index", "Projetos");
            return View(lista);
        }

        //[HttpPost]
        //public ActionResult ListarTarefas(ColaboradorProjeto colaboradorProjeto)
        //{
        //    return RedirectToAction("Index", "Projetos");
        //}


        public ActionResult VerificarTarefas(int id, string view)
        {

            try
            {
                //var Colaborador = ColaboradoresDao.BuscarColaborador(id);
                var ColaboradorProjeto = ColaboradorProjetoDao.BuscarTarefas(id);
                if (ColaboradorProjeto == null)
                {
                    throw new Exception("Colaborador não existe, ou documento não informado");
                }

                return View(view, ColaboradorProjeto);
                //return View(view, Projetos);
                //return RedirectToAction("Index", "Projetos");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }

        }

        [HttpGet]
        public ActionResult AlterarTarefas(int id)
        {
            //ViewBag.ListarTarefa = new SelectList(ColaboradorProjetoDao.ListarTarefas(), "Id", "TarefaColab");

            return VerificarTarefas(id, "AlterarTarefas");

        }

        [HttpPost]
        public ActionResult AlterarTarefas(ColaboradorProjeto colaboradorProjeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                ColaboradorProjetoDao.AlterarTarefas(colaboradorProjeto);

                return RedirectToAction("ListarTarefas");

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }

        public ActionResult MostrarDetalhes(int id)
        {
            return VerificarTarefas(id, "MostrarDetalhes");
        }

        [HttpGet]
        public ActionResult ExcluirTarefas(int id)
        {
            return VerificarTarefas(id, "ExcluirTarefas");
        }

        [HttpPost]
        public ActionResult ExcluirTarefas(ColaboradorProjeto colaboradorProjeto)
        {
            try
            {
                ColaboradorProjetoDao.ExcluirTarefas(colaboradorProjeto);
                return RedirectToAction("ListarTarefas");
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }


        



        

    }
}