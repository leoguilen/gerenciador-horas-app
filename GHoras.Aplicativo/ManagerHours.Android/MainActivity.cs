
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Platform;

namespace ManagerHours.Droid
{
    [Activity(Label = "Gerenciador de Horas", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            InitializeRenderers(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags(new[] { "CollectionView_Experimental", "Shell_Experimental" });
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            LoadApplication(new App());
        }

        public void InitializeRenderers(Bundle bundle)
        {
            CachedImageRenderer.Init(false);
            CarouselViewRenderer.Init();
            Rg.Plugins.Popup.Popup.Init(this,bundle);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}