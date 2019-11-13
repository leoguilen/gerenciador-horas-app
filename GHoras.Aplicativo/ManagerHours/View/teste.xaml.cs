
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManagerHours.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class teste : ContentPage
    {
        private ObservableCollection<GrupoPonto> ListaAgrup { get; set; }
        public teste()
        {
            InitializeComponent();

            ListaAgrup = new ObservableCollection<GrupoPonto>();

            var aviãoCateg = new GrupoPonto() { Mes = "Mês: Novembro" };

            aviãoCateg.Add(new Ponto { Data = "01/11", Saida = "19:55:40", IndicAzul = true, IndicVerde = false, Status = "Horas Normais", CorStatus = "#0366D6" });
            aviãoCateg.Add(new Ponto { Data = "02/11", Saida = "20:01:00", IndicAzul = false, IndicVerde = true, Status = "Com Horas Extras", CorStatus = "#0F9D58" });
            aviãoCateg.Add(new Ponto { Data = "03/11", Saida = "20:54:38", IndicAzul = true, IndicVerde = false, Status = "Horas Normais", CorStatus = "#0366D6" });

            ListaAgrup.Add(aviãoCateg);

            listHistorico.ItemsSource = ListaAgrup;
        }

        private void ListHistorico_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as Ponto;
            DisplayAlert("Valor", $"Valor pressionado: { dataItem.Data}", "OK");
        }
    }

    public class GrupoPonto : ObservableCollection<Ponto>
    {
        public string Mes { get; set; }
    }

    public class Ponto
    {
        public string Data { get; set; }
        public string Saida { get; set; }
        public bool IndicVerde { get; set; }
        public bool IndicAzul { get; set; }
        public string Status { get; set; }
        public string CorStatus { get; set; }

        public Ponto()
        {

        }
    }
}