using Project.Manager.Models;
using Project.Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Manager.DBProject
{
    public class SkillDao
    {
        public static void CadastrarSkill(Skill skill)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Skill.Add(skill);
                ctx.SaveChanges();
            }
        }

        public static IEnumerable<Skill> ListarSkills()
        {
            using (var ctx = new ProjectManagerConnection())
            {

                var habilidade = ctx.Skill.Include(i => i.TBCadColaboradores).ToList();

                return habilidade;
            }
        }

        public static IEnumerable<ColaboradorTypePeopleViewModel> ListarSkillsInfo()
        {
            using (var ctx = new ProjectManagerConnection())
            {

                var habilidade = ctx.Skill.ToList();
                var listadeSkill = new List<ColaboradorTypePeopleViewModel>();
                foreach (var item in habilidade)
                {
                    listadeSkill.Add(new ColaboradorTypePeopleViewModel()
                    {
                        IdSkill = item.Id,
                        TipoSkill = item.TipoSkill,
                        

                    });
                }
                return listadeSkill.ToList();
            }
        }

        //public static IEnumerable<ColaboradorViewModel> ListarSkillInfo()
        //{

        //}



        public static Skill BuscarSkill(int id)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                //Não é aconselhável utilizar o Find nessas condições
                var skill = ctx.Skill.FirstOrDefault(p => p.Id.Equals(id));
                return skill;

                //return colaborador; //ctx.Clientes.FirstOrDefault(p => p.Cpf.Equals(cpf));
            }
        }

        public static void AlterarSkill(Skill skill)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<Skill>(skill).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void ExcluirSkill(Skill skill)
        {
            using (var ctx = new ProjectManagerConnection())
            {
                ctx.Entry<Skill>(skill).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }


    }
}