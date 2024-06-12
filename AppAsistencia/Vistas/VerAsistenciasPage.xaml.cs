using AppAsistencia.Modelos;
using AppAsistencia.VistaModelos;

namespace AppAsistencia.Vistas;

public partial class VerAsistenciasPage : ContentPage
{
	private readonly AsistenciaVM _vistaModelo;
	private readonly Usuario _usuarioAutenticado;
	public VerAsistenciasPage(AsistenciaVM vistaModelo, Usuario usuarioAutenticado)
	{
		InitializeComponent();
		BindingContext = vistaModelo;
		_vistaModelo = vistaModelo;
		_usuarioAutenticado = usuarioAutenticado;
	}

	protected async override void OnAppearing()
	{ 
		base.OnAppearing();
		await _vistaModelo.LoadAsistenciasAsync();
	}

}