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
        //var asistenciaVM = new AsistenciaVM(CrossFingerprint.Current); // necesita un parámetro de tipo IFingerprint
        //asistenciaVM.ValidarBiometrico(); // Aquí se llama la método para autenticación biométrica
    }
}