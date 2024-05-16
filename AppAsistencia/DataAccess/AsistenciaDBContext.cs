// Agregar Modelos, Utilidades y  Microsoft.EF
using System.IO.Compression;
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
        public DbSet<Asistencia> Asistencias { get; set; }

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
                entity.HasKey(u => u.IdUsuario);
                entity.Property(u => u.IdUsuario).IsRequired().ValueGeneratedOnAdd();        
            });

            // Aquí modelar las demás tablas
            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(a => a.IdAsistencia);
                entity.Property(a => a.IdAsistencia).IsRequired().ValueGeneratedOnAdd();
                entity.HasOne(a => a.RefUsuario).WithMany(u => u.Asistencias).HasForeignKey(a => a.IdUsuario);
            });
        }
    }
}
