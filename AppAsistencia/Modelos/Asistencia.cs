using System.ComponentModel.DataAnnotations;

namespace AppAsistencia.Modelos
{
    public class Asistencia
    {
        // Establecer IdAsistencia como Primary Key
        [Key]
        public int IdAsistencia { get; set; }
        public int IdUsuario { get; set;}
        public string FechaAsistencia { get; set; }
        public bool EstadoMarcacionHuellaDactilar { get; set; }
        public string FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public virtual Usuario RefUsuario {get; set; }
    }
}
