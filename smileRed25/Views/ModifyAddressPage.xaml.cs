using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed25.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifyAddressPage : PopupPage
    {       
        public ModifyAddressPage ()
		{
			InitializeComponent ();
           // PAddressLabel.Text = character;
        }

      /*  private async Task Tap_EditNewAddressAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
            Application.Current.MainPage = new ProfilePage();
        }*/
    }
}