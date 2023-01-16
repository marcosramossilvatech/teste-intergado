using GestAgropInter.Models.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestAgropInter.Models
{
    [Table("Fazenda")]
    public class Fazenda: ModelBase
    {


        [StringLength(50)]
        [Required(ErrorMessage ="Nome da fazenda é obrigatorio")]
        [MinLength(5, ErrorMessage = "Nome invalido, muito curto, minimo de  5 caracteres")]
        [MaxLength(100)]
        public string NomeFazenda { get; set; }

        [StringLength(20)]
        public string Endereco { get; set; }

        //public virtual IEnumerable<Animal>? Animais { get; set; }
        public Fazenda()
        {
        }
        public Fazenda(string nomeFazenda, string endereco)
        {
            ValidateDomain(nomeFazenda,  endereco);
        }

        public Fazenda(int id, string nomeFazenda, string endereco)
        {
            DomainExceptionValidation.When(id < 0, "O Id é invalido.");
            Id = id;
            ValidateDomain(nomeFazenda, endereco);
        }

        public void Update(string nomeFazenda, string endereco)
        {
            ValidateDomain(nomeFazenda,  endereco);
        }

        private void ValidateDomain(string nomeFazenda, string endereco)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nomeFazenda),
                "Nome invalido. Nome é obrigatorio.");

            DomainExceptionValidation.When(nomeFazenda.Length < 3,
               "Nome invalido, muito curto, minimo de  5 caracteres");

            DomainExceptionValidation.When(nomeFazenda.Length >50,
               "Nome invalido, muito longo, maximo de 50 caracteres");
            NomeFazenda = nomeFazenda;
            Endereco = endereco;
        }
    }
}
