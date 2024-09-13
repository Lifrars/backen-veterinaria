using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Cliente
{
    [Key]
    public int Cedula { get; set; }

    [Required]
    public string? Nombres { get; set; }

    [Required]
    public string? Apellidos { get; set; }

    [Required]
    public string? Direccion { get; set; }

    [Required]
    public string? Telefono { get; set; }

    public ICollection<Mascota>? Mascotas { get; set; }
}

