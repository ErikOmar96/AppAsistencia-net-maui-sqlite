using AppAsistencia.Vistas;
using Plugin.Fingerprint.Abstractions;

namespace AppAsistencia
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
        }


        private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }

}
