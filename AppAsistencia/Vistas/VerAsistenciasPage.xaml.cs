using AppAsistencia.Modelos;
using AppAsistencia.VistaModelos;
using iTextSharp.text;
using iTextSharp.text.pdf;

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

    private async void btnReportePDF_Clicked(object sender, EventArgs e)
    {
        try
        {
            DateTime fechaInicio = fechainicio.Date;
            DateTime fechaFin = fechafin.Date.AddDays(1).AddTicks(-1);

            var asistencias = await _vistaModelo.GetAsistenciasPorFechasAsync(fechaInicio, fechaFin);

            if (asistencias == null || !asistencias.Any())
            {
                await DisplayAlert("Información", "No se encontraron asistencias en el rango de fechas seleccionado.", "OK");
                return;
            }

            string fileName = $"reporte_asistencias_{ObtenerFechaActual()}.pdf";
            string filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            Paragraph titulo = new Paragraph("REPORTE DE ASISTENCIAS");
            titulo.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(new Paragraph(" "));

            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.AddCell("ID Asistencia");
            table.AddCell("Usuario");
            table.AddCell("Fecha");
            table.AddCell("Estado");

            foreach (var asistencia in asistencias)
            {
                table.AddCell(asistencia.IdAsistencia.ToString());
                //table.AddCell(asistencia.Usuario?.NombreUsuario ?? "N/A");
                table.AddCell(asistencia.Usuario?.NombreUsuario);
                table.AddCell(asistencia.FechaAsistencia.ToString("yyyy-MM-dd HH:mm"));
                table.AddCell(asistencia.EstadoAsistencia);
            }

            doc.Add(table);
            doc.Close();
            writer.Close();

            await DisplayAlert("Información", "El reporte se ha generado correctamente.", "OK");

            try
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            catch (Exception)
            {
                await DisplayAlert("Información", "Para visualizar el contenido del reporte, debe instalar un visor de PDF.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al generar el reporte: {ex.Message}", "OK");
        }
    }

	private string ObtenerFechaActual()
	{ 
		DateTime fechaHora = DateTime.Now;
		return fechaHora.ToString("yyyyy-MM-dd_HH-mm-ss");
	}
}