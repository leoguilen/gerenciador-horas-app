using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : TabbedPage
    {
        public List<string> People { get; set; }
        public Home()
        {
            InitializeComponent();

            People = new List<string>
            {
                "Ultimo Ponto (Data/Hora)",
                "Valor das Horas (R$)",
                "Saída + Tarde (Hora)",
                "Media do horario de saida"
            };

            CV.BindingContext = this;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("TESTE", "Teste", "OK");
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            DisplayAlert("TESTE", "Teste", "OK");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("TESTE", "REFRESHING", "OK");
        }

        private async void Icon_Clicked(object sender, EventArgs e)
        {
            icon.IsVisible = false;
            gif.IsVisible = true;

            await Task.Delay(3000);

            gif.IsVisible = false;
            icon.IsVisible = true;
        }
    }
}