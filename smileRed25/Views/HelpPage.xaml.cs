using smileRed25.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed25.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HelpPage : ContentPage
	{
		public HelpPage ()
		{
			InitializeComponent ();
		}

        private async void Button_Open(object sender, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();          
            mainViewModel.PasswordRecovery = new PasswordRecoveryViewModel();
            //Application.Current.MainPage = new NavigationPage(
            //new PasswordRecovery());
            await Application.Current.MainPage.Navigation.PushAsync(new PasswordRecovery());
        }
    }
}