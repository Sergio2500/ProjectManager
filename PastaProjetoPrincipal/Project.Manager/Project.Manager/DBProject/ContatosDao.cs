using Project.Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Manager.DBProject
{
    public class ContatosDao
    {

        public static void IncluirContato(Contato contato)
        {
            using (var ctx = new ProjectManagerConnection())
            {                
                ctx.Contato.Add(contato);
                ctx.SaveChanges();
            }
        }

        public static IEnumerable<Contato> ListarContatos()
        {
            using (var ctx = new ProjectManagerConnection())
            {
                var lista = ctx.Contato.Include(i => i.TBCadClientes).ToList();

                return lista;                
            }
        }

        public static Contato BuscarContatos(int id)
        {
            using(var ctx = new ProjectManagerConnection())
            {                
                var contato = ctx.Contato.FirstOrDefault(i => i.Id.Equals(id));

                return contato;
            }
        }

        public static void AlterarContatos(Contato contato)
        {
            using(var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<Contato>(contato).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void ExcluirContatos(Contato contato)
        {
            using(var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<Contato>(contato).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }





    }
}