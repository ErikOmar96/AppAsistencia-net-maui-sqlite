using AppAsistencia.DataAccess;

namespace AppAsistencia.Vistas;

public partial class RegistroPage : ContentPage
{
	// Variable para referenciar a la base de datos
	private readonly AsistenciaDBContext _dbContext;

	public RegistroPage(AsistenciaDBContext dBContext)
	{
        _dbContext = dBContext;
        InitializeComponent();
		
		//_dbContext.Usuarios.Add(new Modelos.Usuario)
	}

    private void btnRegistrar_Clicked(object sender, EventArgs e)
    {
        string nombre = txtNombre.Text;
        string clave = txtClave.Text;
		registrarUsuario(nombre, clave);
    }

    private void registrarUsuario(string nombreUsuario, string claveUsuario)
    {
        
    }
}