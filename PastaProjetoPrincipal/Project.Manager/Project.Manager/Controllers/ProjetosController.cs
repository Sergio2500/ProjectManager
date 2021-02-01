using PagedList;
using Project.Manager.DBProject;
using Project.Manager.Models;
using Project.Manager.Struct;
using Project.Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Manager.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ProjetosController : Controller
    {
        // GET: Projetos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CadastrarProjeto()
        {
            ViewBag.ListarClientes = new SelectList(ClientesDao.ListarClientesDois(), "Id", "Descricao");
            ViewBag.ListClient = new SelectList(ClientesDao.ListarClientesDois(), "Id", "Descricao");

            var lista = new List<TipoPgtoProj>()
                {
                    new TipoPgtoProj{Tipo = 1, Descricao = "Valor variável"},
                    new TipoPgtoProj{Tipo = 2, Descricao = "Valor Fixo"}
                };

            ViewBag.ListarTipoProjeto = new SelectList(lista, "Tipo", "Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarProjeto(CadProjeto projeto)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View();
                }
                ProjetosDao.CadastrarProjeto(projeto);

                
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }

        [HttpGet]
        public ActionResult ListarProjetos(string sortOrder, string filtro, string busca, int? page)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "Nome_Desc" : "";
                ViewBag.DateParam = sortOrder == "Date" ? "Date_Desc" : "Date";

                if (busca != null)
                {
                    page = 1;
                }
                else
                {
                    busca = filtro;
                }

                ViewBag.CurrentFilter = busca;


                IEnumerable<CadProjeto> projetos = ProjetosDao.ListarProjetos();
                

                if (!String.IsNullOrEmpty(busca))
                {
                    projetos = projetos.Where(s => s.Descricao.ToUpper().Contains(busca.ToUpper()) ||
                        s.Situacao.ToUpper().Contains(busca.ToUpper()));
                }

                switch (sortOrder)
                {
                    case "Nome_Desc":
                        projetos = projetos.OrderByDescending(s => s.Descricao); break;

                    case "Data":
                        projetos = projetos.OrderBy(s => s.Situacao); break;

                    case "Data_Desc":
                        projetos = projetos.OrderByDescending(s => s.Situacao); break;

                    default:
                        projetos = projetos.OrderBy(s => s.Descricao); break;
                }

                int numRegistro = 5;
                int numPagina = (page ?? 1);

                return View(projetos.ToPagedList(numPagina, numRegistro));

            }

            //ViewBag.ListClient = new SelectList(ClientesDao.ListarClientesDois(), "IdCliente", "Descricao");
            //ViewBag.ListarProjetos = ProjetosDao.ListarProjetosInfo();
            //ViewBag.ListaTipoSkill = SkillDao.ListarSkillsInfo().ToList();
            //ViewBag.ListaTipoPessoa = ColaboradoresDao.ListarColaboradoresInfo();





            //var listaProjeto = ProjetosDao.ListarProjetos();
            //return View(listaProjeto);
        }

        private ActionResult VerificarProjeto(int id, string view)
        {
            try
            {
                var Projeto = ProjetosDao.BuscarProjeto(id);

                if (Projeto == null)
                {
                    throw new Exception("Projeto não existe");
                }

                return View(view, Projeto);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }

        [HttpGet]
        public ActionResult AlterarProjeto(int id)
        {
            //ViewBag.ListarHabilidades = new SelectList(SkillDao.ListarSkills(), "Id", "TipoSkill");

            ViewBag.ListarProjetos = new SelectList(ProjetosDao.ListarProjetos(), "Id", "Descricao");

            //var lista = new List<TipoPessoa>()
            //{
            //    new TipoPessoa{Codigo = 1, Descricao = "Pessoa Física"},
            //    new TipoPessoa{Codigo = 2, Descricao = "Pessoa Jurídica"}
            //};

            //ViewBag.ListarTipoPessoa = new SelectList(lista, "Codigo", "Descricao");

            var lista = new List<TipoPgtoProj>()
            {
                new TipoPgtoProj{Tipo = 1, Descricao = "Valor Variável"},
                new TipoPgtoProj{Tipo = 2, Descricao = "Valor Fixo"}
            };

            ViewBag.ListarTipoPagamento = new SelectList(lista, "Tipo", "Descricao");

            return VerificarProjeto(id, "AlterarProjeto");
        }

        [HttpPost]
        public ActionResult AlterarProjeto(CadProjeto projeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                
                ProjetosDao.AlterarProjeto(projeto);
                return RedirectToAction("ListarProjetos");

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult MostrarDetalhes(int id)
        {
            //return VerificarProjeto(id, "MostrarDetalhes");
            if (ModelState.IsValid)
            {
                var projeto = ProjetosDao.BuscarProjeto(id);
                var horasConsumidas = ProjetosDao.HorasConsumidasNoProjeto(id);
                var x = ProjetosDao.ValorPagoAosColaboradores(projeto.Id);

                HorasFirstViewModel proj;

                if (projeto.TipoPgtoProj == 2)
                {
                    proj = new HorasFirstViewModel()
                    {
                        IdCliente = projeto.TBCadClientes.RazaoSocial,
                        Descricao = projeto.Descricao,
                        NumeroHoras = projeto.NumeroHoras,
                        DataInicio = projeto.DataInicio,
                        DataTermino = projeto.DataTermino,
                        Situacao = projeto.Situacao,
                        TipoPgtoProj = projeto.TipoPgtoProj,
                        Orcamento = projeto.Orcamento,
                        ValorDespesas = projeto.ValorDespesas,
                        HorasRestantes = projeto.NumeroHoras - horasConsumidas,
                        VtotalPagoColabs = ProjetosDao.ValorPagoAosColaboradores(id),
                        Lucro = projeto.Orcamento - projeto.ValorDespesas - x
                    }; 
                }
                else
                {
                    proj = new HorasFirstViewModel()
                    {
                        IdCliente = projeto.TBCadClientes.RazaoSocial,
                        Descricao = projeto.Descricao,
                        NumeroHoras = projeto.NumeroHoras,
                        DataInicio = projeto.DataInicio,
                        DataTermino = projeto.DataTermino,
                        Situacao = projeto.Situacao,
                        TipoPgtoProj = projeto.TipoPgtoProj,
                        Orcamento = projeto.Orcamento,
                        ValorDespesas = projeto.ValorDespesas,
                        HorasRestantes = projeto.NumeroHoras - horasConsumidas,
                        VtotalPagoColabs = ProjetosDao.ValorPagoAosColaboradores(id),
                        Lucro = projeto.Orcamento * projeto.NumeroHoras - projeto.ValorDespesas - x
                    };
                }

                return View(proj);
            }
            else
            {
                return RedirectToAction("ListarProjetos");
            }



        }

        [HttpGet]
        public ActionResult ExcluirProjeto(int id)
        {
            return VerificarProjeto(id, "ExcluirProjeto");
        }

        [HttpPost]
        public ActionResult ExcluirProjeto(CadProjeto projeto)
        {
            try
            {
                ProjetosDao.ExcluirProjeto(projeto);
                return RedirectToAction("ListarProjetos");
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult ListarProjetoColaborador(int id)
        {
            var lista = ColaboradorProjetoDao.ListarColaboradorProjeto(id);
            return View(lista);
        }

        public ActionResult HorasColaborador(int id)
        {
            var lista = ProjetosDao.HorasColaborador(id);

            return View(lista);

        }

        [HttpGet]
        public ActionResult CadastrarHora(int id)
        {
            //ViewBag.Listar = HorasDao.ListarHora();
            ViewBag.Listar = ControlePontoDao.ListarPontos();

            //var colab = ColabProjetoDao.BuscarProjetoColab(id);
            var colab = ColaboradorProjetoDao.BuscarTarefas(id);
            //var hora = new HoraProjeto() { IdColab = colab.IdColabProjeto };
            var hora = new HoraTrabalhada()
            {
                IdColab = colab.Id
            };
            return View(hora);
        }

        [HttpPost]
        public ActionResult CadastrarHora(HoraTrabalhada hora, int id)
        {
            ControlePontoDao.IncluirPonto(hora);
            //HorasDao.CadastrarHora(hora);
            return RedirectToAction("IncluirPonto", id);
            //return RedirectToAction("CadastrarHora", id);
        }




    }
}