using AppAsistencia.DataAccess;
using AppAsistencia.VistaModelos;
using AppAsistencia.Modelos;
namespace AppAsistencia.Vistas;

public partial class RegistroPage : ContentPage
{
	// Variable para referenciar a la base de datos
	private readonly AsistenciaDBContext _dbContext;

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
            // Variable para guardar objeto de UsuarioVM
            var usuariovm = new UsuarioVM(_dbContext);

            //await usuariovm.RegistrarUsuario(new Usuario
            //{
            //    IdUsuario = 0,
            //    NombreUsuario = txtNombre.Text,
            //    ClaveUsuario = txtClave.Text,
            //    CorreoUsuario = txtCorreo.Text
            //});

            // Variable para guardar el valor booleano si hay un usuario no registrado
            var noRegistrado = await usuariovm.RegistrarUsuario(new Usuario
            {
                IdUsuario = 0,
                NombreUsuario = txtNombre.Text,
                ClaveUsuario = txtClave.Text,
                CorreoUsuario = txtCorreo.Text
            });

            if (noRegistrado)
            {
                await DisplayAlert("Alerta", "Usuario Registrado", "Aceptar");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("ERROR", "Ya existe un usuario y correo registrado", "Aceptar");
            }
            //MainThread.BeginInvokeOnMainThread(async () =>
            //{
            //await DisplayAlert("Alerta", "Usuario Registrado", "Aceptar");
            // Regresar al login
            //await Navigation.PopAsync();
            //});
        }
        catch(Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Aceptar");
        }
    }

}