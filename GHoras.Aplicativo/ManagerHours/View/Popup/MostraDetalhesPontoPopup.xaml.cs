using ManagerHours.Dependencies;
using ManagerHours.Services;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace ManagerHours.View.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MostraDetalhesPontoPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private GetRow getRow;

        public MostraDetalhesPontoPopup(string data)
        {
            InitializeComponent();

            getRow = new GetRow(RowServiceDependencies.Inject());

            CarregaInformacoes(data);
        }

        public async void CarregaInformacoes(string data)
        {
            var getRowTask = Task.FromResult(await getRow.GetRowAsync(data));

            if (getRowTask.IsCompleted)
            {
                var rowValue = getRowTask.Result;

                lblData.Text += data;
                lblEntrada.Text += rowValue.Entrada.HasValue ? rowValue.Entrada.Value.ToLongTimeString() : "-";
                lblSaidaAlm.Text += rowValue.SaidaAlmoco.HasValue ? rowValue.SaidaAlmoco.Value.ToLongTimeString() : "-";
                lblEntradaAlm.Text += rowValue.EntradaAlmoco.HasValue ? rowValue.EntradaAlmoco.Value.ToLongTimeString() : "-";
                lblSaida.Text += rowValue.Saida.HasValue ? rowValue.Saida.Value.ToLongTimeString() : "-";
                lblExtras.Text += rowValue.HorasExtras.HasValue ? rowValue.HorasExtras.Value.ToShortTimeString() : "-";
            }
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
        }
    }
}