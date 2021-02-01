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
    public class SkillController : Controller
    {
        // GET: Skill
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CadastrarSkill()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarSkill(Skill skill)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }
                SkillDao.CadastrarSkill(skill);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }




        [HttpGet]
        public ActionResult ListarSkills(/*string sortOrder, string filtro,  string busca, int? pagina */)
        {
            //using (var ctx = new ProjectManagerConnection())
            //{
            //    ViewBag.CurrentSort = sortOrder;
            //    ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "Nome_Desc" : "";
            //    ViewBag.DateParam = sortOrder == "Date" ? "Date_Desc" : "Date";

            //    if (busca != null)
            //    {
            //        pagina = 1;
            //    }
            //    else
            //    {
            //        busca = filtro;
            //    }

            //    ViewBag.CurrentFilter = busca;

            //    var skills = from s in ctx.Skill
            //                 select s;

            //    if (!String.IsNullOrEmpty(busca))
            //    {
            //        skills = skills.Where(s => s.)
            //    }

            //}


            var listaSkill = SkillDao.ListarSkills();
            return View(listaSkill);
        }

        private ActionResult VerificarSkills(int id, string view)
        {
            try
            {
                //var Colaborador = ColaboradoresDao.BuscarColaborador(id);
                var skills = SkillDao.BuscarSkill(id);

                if (skills == null)
                {
                    throw new Exception("Este tipo de Skill não consta no sistema");
                }

                return View(view, skills);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }

        [HttpGet]
        public ActionResult AlterarSkills(int id)
        {
            ViewBag.ListarSkills = new SelectList(SkillDao.ListarSkills(), "Id", "TipoSkill");

            //var lista = new List<TipoPessoa>()
            //{
            //    new TipoPessoa{Codigo = 1, Descricao = "Pessoa Física"},
            //    new TipoPessoa{Codigo = 2, Descricao = "Pessoa Jurídica"}
            //};

            //ViewBag.ListarTipoPessoa = new SelectList(lista, "Codigo", "Descricao");

            return VerificarSkills(id, "AlterarSkills");
        }

        [HttpPost]
        public ActionResult AlterarSkills(Skill skill)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                
                SkillDao.AlterarSkill(skill);

                return RedirectToAction("ListarSkills");

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult MostrarDetalhes(int id)
        {
            return VerificarSkills(id, "MostrarDetalhes");
        }

        [HttpGet]
        public ActionResult ExcluirSkills(int id)
        {
            return VerificarSkills(id, "ExcluirSkills");
        }

        [HttpPost]
        public ActionResult ExcluirSkills(Skill skill)
        {
            try
            {
                //ColaboradoresDao.ExcluirColaborador(skill);
                SkillDao.ExcluirSkill(skill);
                return RedirectToAction("ListarSkills");
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }







    }
}