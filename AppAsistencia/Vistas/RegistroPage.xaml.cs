using AppAsistencia.DataAccess;
using AppAsistencia.VistaModelos;
using AppAsistencia.Modelos;


namespace AppAsistencia.Vistas;

public partial class RegistroPage : ContentPage
{
	// Variable para referenciar a la base de datos
	private readonly AsistenciaDBContext _dbContext;
    private bool _esAdministrador = false;

    // Constructor
    public RegistroPage(AsistenciaDBContext dBContext)
	{
        _dbContext = dBContext;
        InitializeComponent();				
	}

    private async void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        try
        {
            //// Variable para guardar objeto de UsuarioVM
            //var usuariovm = new UsuarioVM(_dbContext);

            //// Variable para guardar el valor booleano si hay un usuario no registrado
            //var noRegistrado = await usuariovm.RegistrarUsuario(new Usuario
            //{
            //    IdUsuario = 0,
            //    NombreUsuario = txtNombre.Text,
            //    ClaveUsuario = txtClave.Text,
            //    CorreoUsuario = txtCorreo.Text

            //});

            //if (noRegistrado)
            //{
            //    await DisplayAlert("Alerta", "Usuario Registrado", "Aceptar");
            //    await Navigation.PopAsync();
            //}
            //else
            //{
            //    await DisplayAlert("ERROR", "Ya existe un usuario y correo registrado", "Aceptar");
            //}
            // Variable para guardar objeto de UsuarioVM
            var usuariovm = new UsuarioVM(_dbContext);

            // Asumir que el registro por defecto es de tipo "Usuario"
            var nuevoUsuario = new Usuario
            {
                IdUsuario = 0,
                NombreUsuario = txtNombre.Text,
                ClaveUsuario = txtClave.Text,
                CorreoUsuario = txtCorreo.Text,
                TipoUsuario = _esAdministrador ? "Administrador" : "Usuario"// Registro por defecto como usuario normal
            };
          

            // Intentar registrar el usuario
            var noRegistrado = await usuariovm.RegistrarUsuario(nuevoUsuario);

            if (noRegistrado)
            {
                await DisplayAlert("Alerta", "Usuario Registrado", "Aceptar");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR", "Ya existe un administrador registrado o el nombre de usuario/correo ya está en uso", "Aceptar");
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Aceptar");
        }
    }

    private void chkEsAdministrador_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _esAdministrador = e.Value;
    }
}