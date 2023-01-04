using System.ComponentModel.DataAnnotations;

namespace GestAgropInter.Models
{
    public abstract class ModelBase
    {
        [Key]
        public int? Id { get; set; }

        public DateTime DataInclusao { get; set; } = DateTime.Now;

        public DateTime? DataAlteracao { get; set; }
    }
}
