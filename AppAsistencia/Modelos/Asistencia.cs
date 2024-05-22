using SQLite;

namespace AppAsistencia.Modelos
{
    public class Asistencia
    {
        [PrimaryKey, AutoIncrement]
        public int IdAsistencia { get; set; }
        public DateTime FechaAsistencia { get; set; }
        public string EstadoAsistencia { get; set; }
        public string TextoAsistencia { get; set; }
        public int IdUsuario {  get; set; } // Foreign Key
        public Asistencia Clone() => MemberwiseClone() as Asistencia;
        

        [Ignore]
        public Usuario Usuario { get; set; }


    }
}
