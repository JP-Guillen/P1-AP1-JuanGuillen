using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P1_AP1_JuanGuillen.Models;

public class TiposHuacales
{
        [Key]
        public int TipoId { get; set; }

        [Required(ErrorMessage = "Debe de tener una Descripcion Obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "La Existencia debe ser Mayor o igual a 0")]
        public int Existencia { get; set; }

        [InverseProperty("TipoHuacal")]
        public virtual ICollection<EntradasHuacalesDetalle> EntradasHuacalesDetalle { get; set; } = new List<EntradasHuacalesDetalle>();
}
