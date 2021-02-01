using Project.Manager.Models;
using Project.Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Manager.DBProject
{
    public class ColaboradoresDao
    {

        public static void CadastrarColaborador(CadColaborador colaborador)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.CadColaborador.Add(colaborador);
                ctx.SaveChanges();
            }
        }

        public static IEnumerable<CadColaborador> ListarColaboradores()
        {
            using (var ctx = new ProjectManagerConnection())
            {

                var colaborador = ctx.CadColaborador.Include(i => i.TBColabProj).Include(i => i.TBSkill).ToList();
                //var colaborador = ctx.CadColaborador.Include(i => i.TBSkill).ToList();

                return colaborador;
            }
        }

        public static IEnumerable<ColaboradorTypePeopleViewModel> ListarColaboradoresInfo()
        {
            using (var ctx = new ProjectManagerConnection())
            {

                var TipoPessoa = ctx.CadColaborador.Include(i => i.TBColabProj)
                    .Include(i => i.TBSkill).ToList();
                //.Include(i => i.TBHorasTrabalhadas)
                var listaTipoPessoa = new List<ColaboradorTypePeopleViewModel>();
                foreach (var item in TipoPessoa)
                {
                    listaTipoPessoa.Add(new ColaboradorTypePeopleViewModel()
                    {
                        IdSkill = item.IdSkill,
                        Nome = item.Nome,
                        TipoPessoa = item.TipoDocumento.ToString(),

                    });
                }
                return listaTipoPessoa.ToList();
            }
        }

        public static CadColaborador BuscarColaborador(int id)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                //Não é aconselhável utilizar o Find nessas condições
                var colaborador = ctx.CadColaborador.FirstOrDefault(p => p.Id.Equals(id));
                return colaborador;

                //return colaborador; //ctx.Clientes.FirstOrDefault(p => p.Cpf.Equals(cpf));
            }
        }

        public static void AlterarColaborador(CadColaborador colaborador)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<CadColaborador>(colaborador).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void ExcluirColaborador(CadColaborador colaborador)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<CadColaborador>(colaborador).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }


    }
}


