// Llamar a DataAcces y VistaModelos
using AppAsistencia.DataAccess;
using AppAsistencia.VistaModelos;

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
    {
        // Variable para guardar objeto de UsuarioVM
        var usuario = new UsuarioVM(_dbContext);
        var resultado= usuario.autenticar(txtUsuario.Text, txtClave.Text);
        if(resultado != null)
            await Navigation.PushAsync(new MenuPage());
        else
            await DisplayAlert("Alerta", "El usuario o clave esta incorrecto. Intente de nuevo", "Aceptar");
    }

    private async void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroPage(_dbContext));
    }
}