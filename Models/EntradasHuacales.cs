
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P1_AP1_JuanGuillen.Models;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "La fecha es obligatoria.")]
    public DateTime fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]

    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "En este campo solo se permiten letras. ")]
    public string NombreCliente { get; set; }
    public int cantidad { get; set; }

    public double precio { get; set; }

    public double Importe { get; set; }

    [InverseProperty("EntradaHuacal")]
    public virtual ICollection<EntradasHuacalesDetalle> EntradasHuacalesDetalles { get; set; } = new List<EntradasHuacalesDetalle>();

}
