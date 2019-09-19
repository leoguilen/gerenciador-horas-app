using Android.App;
using Android.OS;

namespace GHoras.Aplicativo.Droid
{
    [Activity(Label = "Gerenciador de Horas", Icon = "@mipmap/icon", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            StartActivity(typeof(MainActivity));
        }
    }
}