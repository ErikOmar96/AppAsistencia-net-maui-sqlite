using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using AppAsistencia.DataAccess;
using AppAsistencia.DTOs;
using AppAsistencia.Utilidades;
using AppAsistencia.Modelos;
using System.Threading;

namespace AppAsistencia.VistaModelos
{
    public partial class UsuarioVM : ObservableObject /*, IQueryAttributable*/
    {
        // Crear propiedades para el registro
        private readonly AsistenciaDBContext _dbContext;
        
        //[ObservableProperty]
        //private UsuarioDTO usuarioDTO = new UsuarioDTO();
        // Campo para el idUsuario
        public int id;

        // Constructor para inicializar el campo AsistenciaDBContext
        public UsuarioVM(AsistenciaDBContext context)
        {
            _dbContext = context;
        }
        // 
        private int IdUsuario;
        /*public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
        */
        
        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            // Verificar si ya existe un usuario con el mismo nombre de usuario o correo electrónico
            bool usuarioExistente = await _dbContext.Usuarios.AnyAsync(u => u.NombreUsuario == usuario.NombreUsuario || u.CorreoUsuario == usuario.CorreoUsuario);

            if (usuarioExistente)
            {
                // El usuario ya existe, no se puede registrar
                return false;
            }
            else
            {
                // No existe un usuario con el mismo nombre de usuario o correo electrónico, por lo que se puede registrar
                _dbContext.Usuarios.Add(usuario);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            //await Task.Run(async () =>
            //{
            //    if (usuario.IdUsuario == 0)
            //    {
            //        // Agregar el objeto usuario al DBSet Usuarios
            //        _dbContext.Usuarios.Add(usuario);
            //        // Guardar los datos hechos en la DB
            //        id = await _dbContext.SaveChangesAsync();
            //    }
            //    else
            //    {
            //        //var found = await localContext.Tareas.FirstAsync(e => e.IdTarea == IdTarea);
            //        //found.NombreTarea = TareaDto.NombreTarea;

            //        //await localContext.SaveChangesAsync();                
            //    }            
            //});
        }

        // Validar login
        public Usuario autenticar(string nombre,string clave)
        {
            // Devolver un objeto de tipo Usuario que coincida con el nombre y clave, caso contrario devuelve null
            return _dbContext.Usuarios.FirstOrDefault(t => t.NombreUsuario == nombre && t.ClaveUsuario == clave);
        }
    }
}
