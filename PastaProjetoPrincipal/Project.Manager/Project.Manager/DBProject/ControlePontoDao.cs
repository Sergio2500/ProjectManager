using Project.Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Manager.DBProject
{
    public class ControlePontoDao
    {

        public static void IncluirPonto(HoraTrabalhada horaTrabalhada)
        {
            using(var ctx = new ProjectManagerConnection())
            {
                ctx.HoraTrabalhada.Add(horaTrabalhada);
                ctx.SaveChanges();
            }
        }

        public static IEnumerable<HoraTrabalhada> ListarPontos()
        {
            using(var ctx = new ProjectManagerConnection())
            {
                var ponto = ctx.HoraTrabalhada.Include(i => i.TBColabProj).Include(i => i.TBColabProj.TBCadColaboradores).ToList();

                return ponto;
            }
        }

        public static HoraTrabalhada BuscarPontos(int id)
        {
            using(var ctx = new ProjectManagerConnection())
            {
                var ponto = ctx.HoraTrabalhada.FirstOrDefault(i => i.Id.Equals(id));

                return ponto;
            }
        }

        public static void AlterarPonto(HoraTrabalhada hora)
        {
            using (var ctx = new ProjectManagerConnection())
            {                
                ctx.Entry<HoraTrabalhada>(hora).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void ExcluirPonto(HoraTrabalhada hora)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<HoraTrabalhada>(hora).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        //public static int TotalHorasProjeto(int id)
        //{
        //    using (var ctx = new ProjectManagerConnection())
        //    {
                
        //    }
        //}




    }
}