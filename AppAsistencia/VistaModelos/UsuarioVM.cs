using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

//using Microsoft.EntityFrameworkCore;
using AppAsistencia.DataAccess;
//using AppAsistencia.DTOs;
using AppAsistencia.Utilidades;
using AppAsistencia.Modelos;
using System.Threading;

namespace AppAsistencia.VistaModelos
{
    public partial class UsuarioVM : ObservableObject /*, IQueryAttributable*/
    {
        // Crear propiedades para el registro
        private readonly AsistenciaDBContext _dbContext;

        // Campo para el idUsuario
        public int Id { get; set; }

        // Constructor para inicializar el campo AsistenciaDBContext
        public UsuarioVM(AsistenciaDBContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            // Verificar si ya existe un usuario con el mismo nombre de usuario o correo electrónico
            var usuariosExistentes = await _dbContext.GetFilteredAsync<Usuario>(u => u.NombreUsuario == usuario.NombreUsuario || u.CorreoUsuario == usuario.CorreoUsuario);

            if (usuariosExistentes.Any())
            {
                // El usuario ya existe, no se puede registrar
                return false;
            }
            else
            {
                // No existe un usuario con el mismo nombre de usuario o correo electrónico, por lo que se puede registrar
                bool result = await _dbContext.AddItemAsync(usuario);
                return result;
            }
        }

        // Método para autenticar un usuario
        public async Task<Usuario> AutenticarAsync(string nombre, string clave)
        {
            var usuarios = await _dbContext.GetFilteredAsync<Usuario>(t => t.NombreUsuario == nombre && t.ClaveUsuario == clave);
            return usuarios.FirstOrDefault();
        }
    }
}
