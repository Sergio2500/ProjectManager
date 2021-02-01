using Project.Manager.Models;
using Project.Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Manager.DBProject
{
    public class ProjetosDao
    {

        public static void CadastrarProjeto(CadProjeto projeto)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.CadProjeto.Add(projeto);
                ctx.SaveChanges();
            }
        }

        public static IEnumerable<CadProjeto> ListarProjetos()
        {
            using (var ctx = new ProjectManagerConnection())
            {

                var projeto = ctx.CadProjeto.Include(i => i.TBCadClientes).Include(i => i.TBColabProj).ToList();

                return projeto;
            }
        }

        

        public static CadProjeto BuscarProjeto(int id)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                //Não é aconselhável utilizar o Find nessas condições
                var projeto = ctx.CadProjeto.Include(c => c.TBCadClientes).FirstOrDefault(p => p.Id.Equals(id));
                return projeto;

                //return colaborador; //ctx.Clientes.FirstOrDefault(p => p.Cpf.Equals(cpf));
            }
        }

        public static void AlterarProjeto(CadProjeto projeto)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<CadProjeto>(projeto).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void ExcluirProjeto(CadProjeto projeto)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<CadProjeto>(projeto).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        //Esse método calcula quantas horas apenas um colaborador trabalhou em um determinado projeto
        public static int HorasColaborador(int id)//recebe o id da tabela associativa
        {
            using (var ctx = new ProjectManagerConnection())
            {
                var registros = ctx.HoraTrabalhada.Where(i => i.IdColab == id).ToList();
                if(registros.Count > 0)
                {
                    var horasColab = ctx.HoraTrabalhada
                        .GroupBy(x => x.IdColab)
                        .FirstOrDefault(x => x.Key == id)
                        .Sum(x => x.HorarioSaida.Subtract(x.HorarioEntrada).TotalHours);
                    return (int)horasColab;
                }
                return 0;
            }
        }

        //Calcula o total de horas parcial do projeto
        public static int HorasConsumidasNoProjeto(int id)//está recebendo o Id do CadProjeto
        {
            using (var ctx = new ProjectManagerConnection())
            {
                var colabProj = ctx.ColaboradorProjeto
                    .Where(i => i.IdProjeto == id).ToList();//Lista de tabela associativa onde temos o projeto recebido                

                var soma = 0;
                foreach (var item in colabProj)
                {
                    soma += HorasColaborador(item.Id);
                }
                return soma;
            }
        }

        public static double ValorPagoAosColaboradores(int id)
        {
            using(var ctx = new ProjectManagerConnection())
            {
                var colabproj = ctx.ColaboradorProjeto.Where(i => i.IdProjeto == id).ToList();
                double x = 0;

                foreach (var item in colabproj)
                {
                    x += HorasColaborador(item.Id) * item.ValorHora;
                }
                return x;
            }
        }














        //public static int TotalHoras(int id)
        //{
        //    using(var ctx = new ProjectManagerConnection())
        //    {
        //        var colabproj = ctx.ColaboradorProjeto.FirstOrDefault(i => i.IdProjeto == id).TBCadProjeto.NumeroHoras;

        //        return colabproj;

        //    }
        //}




    }
}