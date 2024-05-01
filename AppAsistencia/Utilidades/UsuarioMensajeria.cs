using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AppAsistencia.Utilidades
{
    public class UsuarioMensajeria : ValueChangedMessage<UsuarioMensaje>
    {
        public UsuarioMensajeria(UsuarioMensaje value) : base(value)
        { 
            
        }
    }
}
