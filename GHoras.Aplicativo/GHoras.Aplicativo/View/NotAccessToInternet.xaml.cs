using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System;

namespace GHoras.Aplicativo.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotAccessToInternet : ContentPage
    {
        public NotAccessToInternet()
        {
            InitializeComponent();
        }

        public bool CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
                return true;
            else
                return false;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            lblCheckNet.IsVisible = true;

            await Task.Delay(2000);

            if (CheckConnection())
            {
                Application.Current.MainPage = new NavigationPage(new Dashboard());
            }
            else
            {
                lblCheckNet.IsVisible = false;
                await DisplayAlert("Erro de conexão", "Ainda não identificamos o acesso a internet", "OK");
                return;
            }
        }
    }
}