//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Manager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class HoraTrabalhada
    {
        public int Id { get; set; }

        [Display(Name = "Colaborador")]
        public int IdColab { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Hor�rio de Entrada")]
        public System.DateTime HorarioEntrada { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Hor�rio de Sa�da")]
        public System.DateTime HorarioSaida { get; set; }
    
        public virtual ColaboradorProjeto TBColabProj { get; set; }
    }
}

