namespace AppAsistencia.Vistas;

public partial class TardanzaPage : ContentPage
{
    // Propiedades
    public Command LongPressCommand { get; set; }
    private bool _isLongPressTardanza;

	public TardanzaPage()
	{
		InitializeComponent();
        btnJustificarTardanza.IsEnabled = false;
	}

    private async void btnJustificarTardanza_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage());
    }

    private void imgTardanza_Pressed(object sender, EventArgs e)
    {
        _isLongPressTardanza = true;
        DetectarLongPressTardanza();
    }

    private void imgTardanza_Released(object sender, EventArgs e)
    {
        _isLongPressTardanza = false;
    }

    public async void DetectarLongPressTardanza()
    {
        await Task.Delay(3000);
        if (_isLongPressTardanza && !string.IsNullOrEmpty(txtTardanza.Text))
        {
            LongPressCommand = new Command(() => 
            {
                DisplayAlert("AVISO", "Justificación marcada correctamente", "OK");
                btnJustificarTardanza.IsEnabled = true;
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