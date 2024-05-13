using AppAsistencia.VistaModelos;
//using Plugin.Fingerprint;
//using Plugin.Fingerprint.Abstractions;

namespace AppAsistencia.Vistas;

public partial class AsistenciaPage : ContentPage
{
    //private readonly TimeSpan longPressDuration = TimeSpan.FromSeconds(5);
    public Command LongPressCommand { get; set; }
    private bool pulsacionLarga = false;

    public AsistenciaPage()
    {
        InitializeComponent();
        //DetectarLongPress();
    }


    private async void imgAsistencia_Pressed(object sender, EventArgs e)
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
                //if (imgAsistencia.IsPressed)
                //{
                //    DisplayAlert("AVISO", "Asistencia marcada correctamente", "OK");
                //}
                //else
                //{
                //    DisplayAlert("AVISO", "Pulsaci�n corta", "OK");
                //}
            });
            BindingContext = this;
        }
        else
        {
            await DisplayAlert("Pulsaci�n Corta", "La pulsaci�n ha sido corta", "Aceptar");
        }

        //LongPressCommand = new Command(() =>
        //{
        //    DisplayAlert("AVISO", "Asistencia marcada correctamente", "OK");
        //    //if (imgAsistencia.IsPressed)
        //    //{
        //    //    DisplayAlert("AVISO", "Asistencia marcada correctamente", "OK");
        //    //}
        //    //else
        //    //{
        //    //    DisplayAlert("AVISO", "Pulsaci�n corta", "OK");
        //    //}
        //});
        //BindingContext = this;
    }

    private void imgAsistencia_Released(object sender, EventArgs e)
    {
        // Si se libera antes de 5 segundos, no se considera pulsaci�n larga
        pulsacionLarga = false;
        //DisplayAlert("Pulsaci�n Corta", "La pulsaci�n ha sido corta", "Aceptar");
    }
  
}




