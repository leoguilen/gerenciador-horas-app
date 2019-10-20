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
    public partial class LoaderInserindoNovoPontoPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LoaderInserindoNovoPontoPopup()
        {
            InitializeComponent();

            confirmaçãoDeSucesso();
        }

        public async void confirmaçãoDeSucesso()
        {
            await Task.Delay(3000);

            confirmLoader.IsVisible = false;
            confirmedImg.IsVisible = true;

        }
    }
}