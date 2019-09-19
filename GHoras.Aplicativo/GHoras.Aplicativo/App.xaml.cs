using GHoras.Aplicativo.View;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace GHoras.Aplicativo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (CrossConnectivity.Current.IsConnected)
                MainPage = new NavigationPage(new Dashboard());
            else
                MainPage = new NotAccessToInternet();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
