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
        //public Asistencia Clone() => MemberwiseClone() as Asistencia;
        public Asistencia Clone()
        {
            return (Asistencia)this.MemberwiseClone();
        }


        [Ignore]
        public Usuario Usuario { get; set; }

        // Método para ajustar la fecha actual
        public void AjustarHoraActual()
        {
            FechaAsistencia = new DateTime(FechaAsistencia.Year, FechaAsistencia.Month, FechaAsistencia.Day,
                                           DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        // Método de validación
        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (FechaAsistencia == default(DateTime))
            {
                return (false, $"{nameof(FechaAsistencia)} es requerido.");
            }
            else if (FechaAsistencia > DateTime.Now)
            {
                return (false, $"{nameof(FechaAsistencia)} no puede ser una fecha futura.");
            }

            if (string.IsNullOrWhiteSpace(EstadoAsistencia))
            {
                return (false, $"{nameof(EstadoAsistencia)} es requerido.");
            }
            else
            {
                var estadosValidos = new[] { "Presente", "Ausente", "Tarde" };
                if (!Array.Exists(estadosValidos, estado => estado.Equals(EstadoAsistencia, StringComparison.OrdinalIgnoreCase)))
                {
                    return (false, $"{nameof(EstadoAsistencia)} debe ser uno de los siguientes valores: {string.Join(", ", estadosValidos)}.");
                }
            }

            if (string.IsNullOrWhiteSpace(TextoAsistencia))
            {
                return (false, $"{nameof(TextoAsistencia)} es requerido.");
            }

            if (IdUsuario <= 0)
            {
                return (false, $"{nameof(IdUsuario)} debe ser un número positivo.");
            }

            return (true, null);
        }
    }
    
}
