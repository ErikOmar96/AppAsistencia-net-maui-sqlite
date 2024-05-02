using AppAsistencia.DataAccess;
using AppAsistencia.VistaModelos;
using AppAsistencia.Modelos;
namespace AppAsistencia.Vistas;

public partial class RegistroPage : ContentPage
{
	// Variable para referenciar a la base de datos
	private readonly AsistenciaDBContext _dbContext;

	public RegistroPage(AsistenciaDBContext dBContext)
	{
        _dbContext = dBContext;
        InitializeComponent();				
	}

    private async void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        try
        {

            var usuariovm = new UsuarioVM(_dbContext);

            usuariovm.registrar(new Usuario
            {
                IdUsuario = 0,
                NombreUsuario = txtNombre.Text,
                ClaveUsuario = txtClave.Text,
                CorreoUsuario = txtCorreo.Text
            });

            //MainThread.BeginInvokeOnMainThread(async () =>
            //{
            await DisplayAlert("Alerta", "Usuario Registrado "+usuariovm.id.ToString(), "Aceptar");
            await Navigation.PopAsync();
            //});
        }
        catch(Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Aceptar");
        }
    }

}