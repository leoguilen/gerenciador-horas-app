using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class teste_layout : ContentPage
    {
        public List<string> People { get; set; }
        public teste_layout()
        {
            InitializeComponent();
            //People = new List<string>
            //{
            //    "Alan",
            //    "Betty",
            //    "Charles",
            //};
            //CV.BindingContext = this;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            icon.IsVisible = false;
            gif.IsVisible = true;

            await Task.Delay(3000);

            gif.IsVisible = false;
            icon.IsVisible = true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new PopupSample());
        }

        private void Icon_Clicked(object sender, EventArgs e)
        {

        }
    }
}