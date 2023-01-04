using GestAgropInter.Models.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestAgropInter.Models
{
    [Table("Animal")]
    public class Animal : ModelBase
    {

        [StringLength(15)]
        [Required(ErrorMessage = "Tag do animal é obrigatorio")]
        [MinLength(15, ErrorMessage = "Tag invalido, muito curto, minimo de  15 caracteres")]
        [MaxLength(15)]
        public string Tag { get; set; }

        [Required(ErrorMessage = "Fazenda é obrigatorio")]        
        public string NomeFaz { get; set; }

        public virtual IEnumerable<Fazenda>? FazendaList { get; set; }
        public Animal()
        {
        }
        public Animal(string tag)
        {
            ValidateDomain(tag);
        }

        public Animal(int id, string tag, string nomeFaz)
        {
            DomainExceptionValidation.When(id < 0, "O Id é invalido.");
            Id = id;
            ValidateDomain(tag);
            ValidateDomain(nomeFaz);
            NomeFaz = nomeFaz;
        }

        public void Update(string tag)
        {
            ValidateDomain(tag);
        }

        private void ValidateDomain(string tag)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(tag),
                "Tag invalido. Nome é obrigatorio.");

            DomainExceptionValidation.When(tag.Length < 15,
               "Tag invalido, muito curto, minimo de  15 caracteres");

            DomainExceptionValidation.When(tag.Length > 15,
               "Tag invalido, muito longo, maximo de 15 caracteres");
            Tag = tag;
        }
    }
}
