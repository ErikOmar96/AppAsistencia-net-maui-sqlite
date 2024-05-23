using AppAsistencia.DataAccess;
using AppAsistencia.VistaModelos;

namespace AppAsistencia.Vistas;

public partial class AsistenciaPage : ContentPage
{
    // Vraiable para referenciar a la base de datos
    private readonly AsistenciaDBContext _context;
    public AsistenciaPage(AsistenciaDBContext context)
    {
        InitializeComponent();
        _context = context;
    }

    private void btnMarcarAsistencia_Clicked(object sender, EventArgs e)
    {
        //var asistenciaVM = new AsistenciaVM(CrossFingerprint.Current); // necesita un par�metro de tipo IFingerprint
        //asistenciaVM.ValidarBiometrico(); // Aqu� se llama la m�todo para autenticaci�n biom�trica
    }
}