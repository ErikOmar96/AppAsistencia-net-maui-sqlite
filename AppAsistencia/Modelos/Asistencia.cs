using System.ComponentModel.DataAnnotations;

namespace AppAsistencia.Modelos
{
    public class Asistencia
    {
        // Establecer IdAsistencia como Primary Key
        [Key]
        public int IdAsistencia { get; set; }
        public int IdUsuario { get; set;}
        public DateTime FechaAsistencia { get; set; }
        public string? TextoAsistencia { get; set;}
        public string EstadoAsistencia { get; set; }
        //public string FechaRegistro { get; set; }
        //public string UsuarioRegistro { get; set; }
        public virtual Usuario RefUsuario {get; set; }
    }
}
