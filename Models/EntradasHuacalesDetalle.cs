using P1_AP1_JuanGuillen.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EntradasHuacalesDetalle
{
    [Key]
    public int DetallesId { get; set; }
    public int IdEntrada { get; set; }
    public int TipoId { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
    public int Cantidad { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
    public double Precio { get; set; }

    [ForeignKey("IdEntrada")]
    [InverseProperty("EntradasHuacalesDetalles")]
    public virtual EntradasHuacales EntradaHuacal { get; set; } = null;

    [ForeignKey("TipoId")]
    [InverseProperty("EntradasHuacalesDetalle")]
    public virtual TiposHuacales TipoHuacal { get; set; }
}
