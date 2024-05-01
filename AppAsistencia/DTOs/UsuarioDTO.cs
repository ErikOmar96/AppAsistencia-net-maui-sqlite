// Agregar el using del NuGet instalado
using CommunityToolkit.Mvvm.ComponentModel;

namespace AppAsistencia.DTOs
{
    public partial class UsuarioDTO : ObservableObject
    {
        // 1.Copiar propiedades del modelo
        // 2. Agregar notación a cada propiedad
        [ObservableProperty]
        public int idSuario;
        [ObservableProperty]
        public string nombreUsuario;
        [ObservableProperty]
        public string claveUsuario;
        [ObservableProperty]
        public string correoUsuario;
    }
}
