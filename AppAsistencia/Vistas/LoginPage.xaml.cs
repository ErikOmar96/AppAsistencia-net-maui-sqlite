// Llamar a DataAcces y VistaModelos
using AppAsistencia.DataAccess;
using AppAsistencia.VistaModelos;
//using Plugin.Fingerprint.Abstractions;

namespace AppAsistencia.Vistas;

public partial class LoginPage : ContentPage
{
    //
    private readonly AsistenciaDBContext _dbContext;
    public LoginPage()
	{
        // Guardar en la variable, la nueva instancia de AsistenciaDBContext
        _dbContext = new AsistenciaDBContext();
        InitializeComponent();        
	}

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {   // Crear una instancia de UsuarioVM con el contexto de base de datos
        var usuarioVM = new UsuarioVM(_dbContext);

        // Autenticar usuario
        var usuarioAutenticado = await usuarioVM.AutenticarAsync(txtUsuario.Text, txtClave.Text);

        if (usuarioAutenticado != null)
        {
            await DisplayAlert("AVISO", $"Bienvenido {txtUsuario.Text}", "OK");
            await Navigation.PushAsync(new MenuPage(_dbContext));
        }
        else
        {
            await DisplayAlert("Alerta", "El usuario o clave es incorrecto. Intente de nuevo", "Aceptar");
        }
    }

    private async void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroPage(_dbContext));
    }
}