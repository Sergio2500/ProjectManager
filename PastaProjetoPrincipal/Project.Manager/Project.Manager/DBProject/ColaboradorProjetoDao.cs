using Project.Manager.Models;
using Project.Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Manager.DBProject
{
    public class ColaboradorProjetoDao
    {
        public static void IncluirTarefa(ColaboradorProjeto colaboradorProjeto)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.ColaboradorProjeto.Add(colaboradorProjeto);
                ctx.SaveChanges();
            }
        }

        public static IEnumerable<ColaboradorProjeto> ListarTarefas()
        {
            using (var ctx = new ProjectManagerConnection())
            {                
                var colabProj = ctx.ColaboradorProjeto.Include(i => i.TBCadColaboradores).ToList();

                return colabProj;
            }
        }

        public static IEnumerable<ProjetoViewModel> ListarProjetosInfo()
        {
            using (var ctx = new ProjectManagerConnection())
            {

                //var TipoPessoa = ctx.CadColaborador.ToList();
                var projeto = ctx.CadProjeto.ToList();

                //var listaTipoPessoa = new List<ColaboradorTypePeopleViewModel>();
                var listaProjeto = new List<ProjetoViewModel>();

                foreach (var item in projeto)
                {
                    listaProjeto.Add(new ProjetoViewModel()
                    {
                        Id = item.Id,
                        Descricao = item.Descricao,

                    });
                }
                return listaProjeto.ToList();
            }
        }

        public static ColaboradorProjeto BuscarTarefas(int id)
        {
            using (var ctx = new ProjectManagerConnection())
            {                
                var tarefa = ctx.ColaboradorProjeto.FirstOrDefault(p => p.Id.Equals(id));

                return tarefa; 
            }
        }

        public static void AlterarTarefas(ColaboradorProjeto colaboradorProjeto)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<ColaboradorProjeto>(colaboradorProjeto).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void ExcluirTarefas(ColaboradorProjeto colaboradorProjeto)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<ColaboradorProjeto>(colaboradorProjeto).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }


        public static IEnumerable<ColaboradorProjeto> ListarProjetoColaborador(int id)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                
                var colabProj = ctx.ColaboradorProjeto.Where(i => i.IdColab == id)
                    .Include(c => c.TBCadProjeto)
                    .Include(p => p.TBCadColaboradores).ToList();
                return colabProj;
            }
        }

        public static IEnumerable<ColaboradorProjeto> ListarColaboradorProjeto(int id)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                var projetoColab = ctx.ColaboradorProjeto
                    .Where(i => i.IdProjeto == id)
                    .Include(i => i.TBCadColaboradores)
                    .Include(i => i.TBCadProjeto).ToList();
                return projetoColab;
            }
        }

        public static void ExcluirProjetoColaborador(ColaboradorProjeto colaboradorProjeto)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<ColaboradorProjeto>(colaboradorProjeto).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public static IEnumerable<ColabProjNomeViewModel> ListarNomes()
        {
            using (var ctx = new ProjectManagerConnection())
            {
                var nomes = ctx.ColaboradorProjeto.ToList();
                List<ColabProjNomeViewModel> lista = new List<ColabProjNomeViewModel>();

                foreach (var item in nomes)
                {
                    lista.Add(new ColabProjNomeViewModel()
                    {
                        Codigo =item.Id,
                        Nome = item.TBCadProjeto.Descricao + " - " + item.TBCadColaboradores.Nome
                    });
                }

                return lista;

            }
        }




    }
}