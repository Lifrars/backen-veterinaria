using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Medicamento
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Nombre { get; set; }

    [Required]
    public string? Descripcion { get; set; }

    [Required]
    public string? Dosis { get; set; }

    public int MascotaIdentificacion { get; set; }
    
    public Mascota Mascota { get; set; }
}