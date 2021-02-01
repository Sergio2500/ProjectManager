using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Manager.Models
{
    public class UsuarioView
    {
        [Required]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        [Required]
        [Compare("Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar:")]
        public string Confirma { get; set; }

        [Display(Name = "Nível:")]
        public string Nivel { get; set; }
    }
}