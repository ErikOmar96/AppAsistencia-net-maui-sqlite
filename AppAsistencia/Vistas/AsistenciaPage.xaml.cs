using AppAsistencia.VistaModelos;
//using Plugin.Fingerprint;
//using Plugin.Fingerprint.Abstractions;

namespace AppAsistencia.Vistas;

public partial class AsistenciaPage : ContentPage
{
    //private readonly TimeSpan longPressDuration = TimeSpan.FromSeconds(5);
    public Command LongPressCommand { get; set; }
    private bool pulsacionLarga;
    // Obtener la fecha y hora actual
    private DateTime FechaHoraActual = DateTime.Now;

    public AsistenciaPage()
    {
        InitializeComponent();
        //DetectarLongPress();
        btnMarcarAsistencia.IsEnabled = false;
    }


    private void imgAsistencia_Pressed(object sender, EventArgs e)
    {
        pulsacionLarga = true;
        DetectarLongPress();
    }


    public async void DetectarLongPress()
    {
        //// Esperar 3 segundos para una pulsaci�n larga
        await Task.Delay(3000);
        if (pulsacionLarga)
        {
            LongPressCommand = new Command(() =>
            {
                DisplayAlert("AVISO", "Asistencia marcada correctamente", "OK");
                btnMarcarAsistencia.IsEnabled = true;
                
            });
            BindingContext = this;           
        }
        else
        {
            await DisplayAlert("Pulsaci�n Corta", "La pulsaci�n ha sido corta", "Aceptar");
            return;
        }
    }

    private void imgAsistencia_Released(object sender, EventArgs e)
    {
        // Si se libera antes de 5 segundos, no se considera pulsaci�n larga
        pulsacionLarga = false;
        //DetectarLongPress();      
    }

    private async void btnMarcarAsistencia_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage());
    }
}




