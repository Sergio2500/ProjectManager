using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Manager.ViewModel
{
    public class HorasFirstViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Cliente:")]
        public string IdCliente { get; set; }

        [Display(Name ="Descrição:")]
        public string Descricao { get; set; }

        [Display(Name = "Número de Horas:")]
        public int NumeroHoras { get; set; }

        [Display(Name = "Data de Início:")]
        public System.DateTime DataInicio { get; set; }

        [Display(Name = "Data de Finalização:")]
        public System.DateTime DataTermino { get; set; }

        [Display(Name = "Situação:")]
        public string Situacao { get; set; }

        [Display(Name = "Tipo de Pagamento:")]
        public short TipoPgtoProj { get; set; }

        [Display(Name = "Orçamento:")]
        public double Orcamento { get; set; }

        [Display(Name = "Despesas Adicionais:")]
        public double ValorDespesas { get; set; }

        [Display(Name = "Horas restantes:")]
        public int HorasRestantes { get; set; }

        [Display(Name = "Gastos com colaboradores:")]
        public double VtotalPagoColabs { get; set; }

        public double Lucro { get; set; }
    }
}