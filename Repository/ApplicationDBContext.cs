using Microsoft.EntityFrameworkCore;

public class PetContext : DbContext
{
    public PetContext(DbContextOptions<PetContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }
    public DbSet<Medicamento> Medicamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Mascotas)
            .WithOne(m => m.Cliente)
            .HasForeignKey(m => m.ClienteCedula);

        // Configuración de la relación Mascota - Medicamento
        modelBuilder.Entity<Mascota>()
            .HasMany(m => m.Medicamentos)
            .WithOne(med => med.Mascota)
            .HasForeignKey(med => med.MascotaIdentificacion);

        // Datos de prueba
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente { Cedula = 1, Nombres = "Juan", Apellidos = "Pérez", Direccion = "Calle 1", Telefono = "123456789" },
            new Cliente { Cedula = 2, Nombres = "Ana", Apellidos = "García", Direccion = "Calle 2", Telefono = "987654321" }
        );

        modelBuilder.Entity<Mascota>().HasData(
            new Mascota { Identificacion = 1, Nombre = "Rex", Raza = "Labrador", Edad = 5, Peso = 30.5, ClienteCedula = 1 },
            new Mascota { Identificacion = 2, Nombre = "Bella", Raza = "Bulldog", Edad = 3, Peso = 25.0, ClienteCedula = 2 }
        );

        modelBuilder.Entity<Medicamento>().HasData(
            new Medicamento { Id = 1, Nombre = "Antibiotico A", Descripcion = "Antibiotico para infecciones", Dosis = "1 pastilla cada 8 horas", MascotaIdentificacion = 1 },
            new Medicamento { Id = 2, Nombre = "Antiinflamatorio B", Descripcion = "Para inflamaciones", Dosis = "1 pastilla cada 12 horas", MascotaIdentificacion = 1 }
        );
        
    }

}