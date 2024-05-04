namespace AppAsistencia.Vistas;

public partial class MenuPage : ContentPage
{
    
    public MenuPage()
	{
		InitializeComponent();
        
	}

    private async void btnMarcarAsistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AsistenciaPage());
    }

    private void btnJustificarTardanza_Clicked(object sender, EventArgs e)
    {

    }

    private void btnJustificarInasistencia_Clicked(object sender, EventArgs e)
    {

    }

    private void btnVerAsistencias_Clicked(object sender, EventArgs e)
    {

    }

    private void btnActualizarDatos_Clicked(object sender, EventArgs e)
    {

    }

    private void btnSalir_Clicked(object sender, EventArgs e)
    {

    }
}