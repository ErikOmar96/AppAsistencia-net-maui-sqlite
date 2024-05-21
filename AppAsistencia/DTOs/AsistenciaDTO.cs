//Agregar using
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppAsistencia.DTOs
{
    public partial class AsistenciaDTO : ObservableObject
    {
        [ObservableProperty]
        public int idAsistencia;
        [ObservableProperty]
        public DateTime fechaAsistencia;
        [ObservableProperty]
        public string? textoAsistencia;
        [ObservableProperty]
        public string estadoAsistencia;
        //[ObservableProperty]
        //public string fechaRegistro;
       // [ObservableProperty]
        //public string usuarioRegistro;
        [ObservableProperty]
        public UsuarioDTO usuario;
    }
}
