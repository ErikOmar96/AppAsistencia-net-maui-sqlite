// Agregar Modelos, Utilidades y  Microsoft.EF
using AppAsistencia.Modelos;
using AppAsistencia.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace AppAsistencia.DataAccess
{
    // Heredar de DbContext
    public class AsistenciaDBContext : DbContext
    {
        // Crear tabla
        public DbSet<Usuario> Usuarios { get; set; }

        // Sobreescribir método
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConexionDB.DevolverRuta("asistencia.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        // Modelar tablas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(col => col.IdUsuario);
                entity.Property(col => col.IdUsuario).IsRequired().ValueGeneratedOnAdd();
            });

            // Aquí modelar las demás tablas
        }
    }
}
