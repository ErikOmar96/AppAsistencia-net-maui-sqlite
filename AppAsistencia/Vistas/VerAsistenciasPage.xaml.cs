using AppAsistencia.VistaModelos;

namespace AppAsistencia.Vistas;

public partial class VerAsistenciasPage : ContentPage
{
	private readonly AsistenciaVM _vistaModelo;
	public VerAsistenciasPage(AsistenciaVM vistaModelo)
	{
		InitializeComponent();
		BindingContext = vistaModelo;
		_vistaModelo = vistaModelo;
	}

	protected async override void OnAppearing()
	{ 
		base.OnAppearing();
		await _vistaModelo.LoadAsistenciasAsync();
	}
}