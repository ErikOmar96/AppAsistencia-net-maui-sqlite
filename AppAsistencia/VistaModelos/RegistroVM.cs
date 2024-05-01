using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AppAsistencia.DataAccess;
using AppAsistencia.DTOs;
using AppAsistencia.Utilidades;
using AppAsistencia.Modelos;

namespace AppAsistencia.VistaModelos
{
    public partial class RegistroVM : ObservableObject, IQueryAttributable
    {
        // Crear propiedades para el registro
        private readonly AsistenciaDBContext _dbContext;
        [ObservableProperty]
        private UsuarioDTO usuarioDTO = new UsuarioDTO();

        // 
        private int IdUsuario;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
