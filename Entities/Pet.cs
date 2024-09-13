using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Mascota
{
    [Key]
    public int Identificacion { get; set; }

    [Required]
    public string? Nombre { get; set; }

    [Required]
    public string? Raza { get; set; }

    [Required]
    public int Edad { get; set; }

    [Required]
    public double Peso { get; set; }

    [Required]
    public int ClienteCedula { get; set; }

    public Cliente? Cliente { get; set; }

    public ICollection<Medicamento>? Medicamentos { get; set; }= null!;
}


