namespace AppAsistencia.Vistas;

public partial class InasistenciaPage : ContentPage
{
    // Propiedades
    public Command LongPressCommand { get; set; }
    private bool _isLongPressInasistencia;

    public InasistenciaPage()
	{
		InitializeComponent();
        btnJustificarInasistencia.IsEnabled = false;
	}

    private async void btnJustificarInasistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage());
    }

    private void imgInasistencia_Pressed(object sender, EventArgs e)
    {
        _isLongPressInasistencia = true;
        DetectarLongPressInasistencia();
    }

    private void imgInasistencia_Released(object sender, EventArgs e)
    {
        _isLongPressInasistencia = false;
    }

    public async void DetectarLongPressInasistencia()
    {
        await Task.Delay(3000);
        if (_isLongPressInasistencia && !string.IsNullOrEmpty(txtInasistencia.Text))
        {
            LongPressCommand = new Command(() =>
            {
                DisplayAlert("AVISO", "Inasistencia justificada correctamente", "OK");
                btnJustificarInasistencia.IsEnabled = true;
            });
            BindingContext = this;
        }
        else
        {
            await DisplayAlert("ERROR", "La pulsación ha sido corta", "OK");
            return;
        }
    }
}