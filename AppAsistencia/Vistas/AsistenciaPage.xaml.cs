using AppAsistencia.VistaModelos;
//using Plugin.Fingerprint;
//using Plugin.Fingerprint.Abstractions;

namespace AppAsistencia.Vistas;

public partial class AsistenciaPage : ContentPage
{
    public AsistenciaPage()
    {
        InitializeComponent();
    }

    private void btnMarcarAsistencia_Clicked(object sender, EventArgs e)
    {
        //var asistenciaVM = new AsistenciaVM(CrossFingerprint.Current); // necesita un parámetro de tipo IFingerprint
        //asistenciaVM.ValidarBiometrico(); // Aquí se llama la método para autenticación biométrica
    }
}