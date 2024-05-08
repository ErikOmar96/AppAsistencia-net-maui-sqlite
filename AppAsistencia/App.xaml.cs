using AppAsistencia.Vistas;
using Plugin.Fingerprint.Abstractions;

namespace AppAsistencia
{
    public partial class App : Application
    {
        public App(IFingerprint fingerprint)
        {
            InitializeComponent();
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
