using Project.Manager.Models;
using Project.Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Manager.DBProject
{
    public class ClientesDao
    {

        public static void IncluirCliente(CadCliente cliente)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.CadCliente.Add(cliente);
                ctx.SaveChanges();
            }
        }


        public static IEnumerable<CadCliente> ListarClientes()
        {
            using (var ctx = new ProjectManagerConnection())
            {

                var clientes = ctx.CadCliente.ToList();

                return clientes;
            }
        }


        public static IEnumerable<ClienteViewModel> ListarClientesDois()
        {
            using (var ctx = new ProjectManagerConnection())
            {
                var cliente = ctx.CadCliente.ToList();

                List<ClienteViewModel> listaModel = new List<ClienteViewModel>();

                foreach (var item in cliente)
                {
                    listaModel.Add(new ClienteViewModel() { Id = item.Id, Descricao = item.RazaoSocial });

                }
                return listaModel;
            }
        }



        public static CadCliente BuscarCliente(int id)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                //Não é aconselhável utilizar o Find nessas condições
                var cliente = ctx.CadCliente.FirstOrDefault(p => p.Id.Equals(id));
                //if (cliente != null)
                //{
                //    List<CadProjeto> projetos = new List<CadProjeto>();

                //    foreach (var item in cliente.TBCadProjeto)
                //    {
                //        projetos.Add(item);
                //    }
                //    cliente.TBCadProjeto = projetos;
                //}

                return cliente; //ctx.Clientes.FirstOrDefault(p => p.Cpf.Equals(cpf));
            }
        }

        public static void AlterarCliente(CadCliente cliente)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<CadCliente>(cliente).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void ExcluirCliente(CadCliente cliente)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<CadCliente>(cliente).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }


    }
}