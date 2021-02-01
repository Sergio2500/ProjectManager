using Project.Management.Mttechne.MetodosValidacao;
using Project.Manager.DBProject;
using Project.Manager.Models;
using System;
using System.Web.Mvc;

namespace Project.Manager.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IncluirCLiente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IncluirCLiente(CadCliente cliente)
        {
            try
            {
                if (!cliente.CNPJ.IsCnpj())
                {
                    ModelState.AddModelError("Cpf", "O CPF informado é inválido");
                }

                if (!ModelState.IsValid)
                {
                    return View();
                }
                ClientesDao.IncluirCliente(cliente);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }
        
        public ActionResult ListarClientes(/*string sortOrder, string filtro, string busca, int? pagina*/)
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

            //    var clientes = from c in ctx.CadCliente
            //                   select c;

            //    if (!String.IsNullOrEmpty(busca))
            //    {
            //        clientes = clientes.Where(i)
            //    }

            //}

            var lista = ClientesDao.ListarClientes();
            return View(lista);
        }

        private ActionResult VerificarCliente(int id, string view)
        {
            try
            {
                var cliente = ClientesDao.BuscarCliente(id);

                if (cliente == null)
                {
                    throw new Exception("Cliente não existe, ou CPF não informado");
                }

                return View(view, cliente);
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");

            }
        }

        [HttpGet]
        public ActionResult AlterarCliente(int id)
        {
            return VerificarCliente(id, "AlterarCliente");
        }

        [HttpPost]
        public ActionResult AlterarCliente(CadCliente cliente, string modo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                ClientesDao.AlterarCliente(cliente);

                if (string.IsNullOrEmpty(modo))
                {
                    return RedirectToAction("ListarClientes");
                }
                
                else
                {
                    throw new Exception("Parametro inválido");
                }
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }

        public ActionResult MostrarDetalhes(int id)
        {
            return VerificarCliente(id, "MostrarDetalhes");
        }

        [HttpGet]
        public ActionResult ExcluirCliente(int id)
        {
            return VerificarCliente(id, "ExcluirCliente");
        }

        [HttpPost]
        public ActionResult ExcluirCliente(CadCliente cliente)
        {
            try
            {
                ClientesDao.ExcluirCliente(cliente);
                return RedirectToAction("ListarClientes");
            }
            catch (Exception ex)
            {

                ViewBag.MensagemErro = ex.Message;
                return View("_Erro");
            }
        }



    }
}