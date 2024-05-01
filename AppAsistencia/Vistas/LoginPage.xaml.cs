using AppAsistencia.DataAccess;

namespace AppAsistencia.Vistas;

public partial class LoginPage : ContentPage
{
    //
    private readonly AsistenciaDBContext _dbContext;
    public LoginPage()
	{
		InitializeComponent();
	}

    private async void btnIngresar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage());
    }

    private async void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistroPage(_dbContext));
    }
}