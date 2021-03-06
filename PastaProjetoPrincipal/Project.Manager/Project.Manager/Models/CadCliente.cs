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

    public partial class CadCliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CadCliente()
        {
            this.TBCadProjeto = new HashSet<CadProjeto>();
            this.TBContatos = new HashSet<Contato>();
        }

        public int Id { get; set; }

        [Display(Name = "Raz�o Social:")]
        public string RazaoSocial { get; set; }
        [Display(Name = "�rea de Atua��o:")]
        public string AreaAtuacao { get; set; }
        [Display(Name = "CNPJ:")]
        public string CNPJ { get; set; }
        [Display(Name = "CEP:")]
        public string CEP { get; set; }
        [Display(Name = "Logradouro:")]
        public string Logradouro { get; set; }
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        [Display(Name = "Cidade:")]
        public string Cidade { get; set; }
        [Display(Name = "UF:")]
        public string UF { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CadProjeto> TBCadProjeto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contato> TBContatos { get; set; }
    }
}
