using System;
using MedTexterApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MedTexterApp
{
	public partial class App : Application
	{
		public static LoginItemManager LoginManager { get; private set; }

		public App()
		{
			InitializeComponent();

            LoginManager = new LoginItemManager(new DocumentDBService());
            MainPage = new masterPage();

            Current.Resources = new ResourceDictionary();
            Current.Resources.Add("UlycesColor", Color.FromRgb(121, 248, 81));
            var navigationStyle = new Style(typeof(NavigationPage));

            var barBackgroundColorSetter = new Setter
            {
                Property = NavigationPage.BarBackgroundColorProperty,
                Value = Color.FromHex("#201E1E")
            };
            navigationStyle.Setters.Add(barBackgroundColorSetter);
            Current.Resources.Add(navigationStyle);
        }

        private void InitializeComponent()
        {
            
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
