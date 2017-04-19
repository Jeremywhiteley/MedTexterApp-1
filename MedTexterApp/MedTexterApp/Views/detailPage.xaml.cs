using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedTexterApp
{
	public partial class detailpage : ContentPage
	{
		bool isLoggingIn = false;
		bool isLoggedIn = false;

		public detailpage()
		{
			InitializeComponent();
		}

        private void InitializeComponent()
        {
            
        }

        protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (!isLoggingIn && !isLoggedIn)
			{
				isLoggingIn = true;
				isLoggedIn = await App.LoginManager.LoginAsync(this);
				isLoggingIn = false;
			}			
		}		
	}
}
