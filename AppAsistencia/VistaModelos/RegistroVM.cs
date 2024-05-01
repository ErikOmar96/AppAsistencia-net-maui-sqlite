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
        
        [ObservableProperty]
        private UsuarioDTO usuarioDTO = new UsuarioDTO();
        public int id;

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
        
        public async void registrar(Usuario usuario)
        {
            await Task.Run(async () =>
            {
                if (usuario.IdUsuario == 0)
                {

                    _dbContext.Usuarios.Add(usuario);
                    id = await _dbContext.SaveChangesAsync();
                }
                else
                {
                    //var found = await localContext.Tareas.FirstAsync(e => e.IdTarea == IdTarea);
                    //found.NombreTarea = TareaDto.NombreTarea;

                    //await localContext.SaveChangesAsync();

                }            
            });
        }


        public Usuario autenticar(string nombre,string clave)
        {            
            return _dbContext.Usuarios.FirstOrDefault(t => t.NombreUsuario == nombre && t.ClaveUsuario == clave);
        }
    }
}
