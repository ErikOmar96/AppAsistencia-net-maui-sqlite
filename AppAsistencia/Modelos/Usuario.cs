using System.ComponentModel.DataAnnotations;

namespace AppAsistencia.Modelos
{
    public class Usuario
    {
        // Establecer IdUsuario como primary key
        [Key]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ClaveUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public virtual ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
    }
}
