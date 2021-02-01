using Project.Manager.DBProject;
using Project.Manager.Models;
using Project.Manager.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Manager.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ColaboradorController : Controller
    {
        // GET: Colaborador
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IncluirColaborador()
        {
            ViewBag.ListarHabilidades = new SelectList(SkillDao.ListarSkills(), "Id", "TipoSkill");

            var lista = new List<TipoPessoa>()
            {
                new TipoPessoa{Codigo = 1, Descricao = "Pessoa Física"},
                new TipoPessoa{Codigo = 2, Descricao = "Pessoa Jurídica"}
            };

            ViewBag.ListarTipoPessoa = new SelectList(lista, "Codigo", "Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult IncluirColaborador(CadColaborador colaborador)
        {
            try
            {
                //if (!colaborador.TipoDocumento.())
                //{
                //    ModelState.AddModelError("Cpf", "O CPF informado é inválido");
                //}                

                if (!ModelState.IsValid)
                {
                    return View();
                }
                ColaboradoresDao.CadastrarColaborador(colaborador);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }

        [HttpGet]
        public ActionResult ListarColaboradores()
        {
            var listaColaborador = ColaboradoresDao.ListarColaboradores();

            ViewBag.ListaTipoSkill = SkillDao.ListarSkillsInfo().ToList();

            ViewBag.ListaTipoPessoa = ColaboradoresDao.ListarColaboradoresInfo();

            return View(listaColaborador);
        }

        private ActionResult VerificarColaborador(int id, string view)
        {
            try
            {
                var Colaborador = ColaboradoresDao.BuscarColaborador(id);

                if (Colaborador == null)
                {
                    throw new Exception("Colaborador não existe, ou documento não informado");
                }

                return View(view, Colaborador);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }

        [HttpGet]
        public ActionResult AlterarColaborador(int id)
        {
            ViewBag.ListarHabilidades = new SelectList(SkillDao.ListarSkills(), "Id", "TipoSkill");

            var lista = new List<TipoPessoa>()
            {
                new TipoPessoa{Codigo = 1, Descricao = "Pessoa Física"},
                new TipoPessoa{Codigo = 2, Descricao = "Pessoa Jurídica"}
            };

            ViewBag.ListarTipoPessoa = new SelectList(lista, "Codigo", "Descricao");

            return VerificarColaborador(id, "AlterarColaborador");
        }

        [HttpPost]
        public ActionResult AlterarColaborador(CadColaborador Colaborador)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                ColaboradoresDao.AlterarColaborador(Colaborador);

                return RedirectToAction("ListarColaboradores");
               
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult MostrarDetalhes(int id)
        {
            ViewBag.ListarHabilidades = new SelectList(SkillDao.ListarSkills(), "Id", "TipoSkill");

            var lista = new List<TipoPessoa>()
            {
                new TipoPessoa{Codigo = 1, Descricao = "Pessoa Física"},
                new TipoPessoa{Codigo = 2, Descricao = "Pessoa Jurídica"}
            };

            ViewBag.ListarTipoPessoa = new SelectList(lista, "Codigo", "Descricao");



            return VerificarColaborador(id, "MostrarDetalhes");
        }

        [HttpGet]
        public ActionResult ExcluirColaborador(int id)
        {
            return VerificarColaborador(id, "ExcluirColaborador");
        }

        [HttpPost]
        public ActionResult ExcluirColaborador(CadColaborador Colaborador)
        {
            try
            {
                ColaboradoresDao.ExcluirColaborador(Colaborador);
                return RedirectToAction("ListarColaboradores");
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }


        public ActionResult ListarColaboradorProjeto(int id)
        {
            var lista = ColaboradorProjetoDao.ListarProjetoColaborador(id);           

            return View(lista);
        }

        public ActionResult ExcluirColaboradorProjeto()
        {
            return View();
        }












    }
}