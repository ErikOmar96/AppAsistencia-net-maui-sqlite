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
        //var asistenciaVM = new AsistenciaVM(CrossFingerprint.Current); // necesita un par�metro de tipo IFingerprint
        //asistenciaVM.ValidarBiometrico(); // Aqu� se llama la m�todo para autenticaci�n biom�trica
    }
}