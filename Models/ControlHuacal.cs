using System.ComponentModel.DataAnnotations;

namespace P1_AP1_JuanGuillen.Models;
public class ControlHuacal
{

    [Key]

    public int IdEntrada { get; set; }

    DateTime fecha { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
    public string NombreCliente { get; set; }
    public int cantidad { get; set; }
    public double precio { get; set; }

}

