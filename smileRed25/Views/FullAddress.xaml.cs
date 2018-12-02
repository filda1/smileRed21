using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using smileRed25.Helpers;
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
	public partial class FullAddress : PopupPage
    {
        #region Properties
    
        #endregion

        #region Constructors
        public FullAddress ()
		{
			InitializeComponent ();
                      
        }
        #endregion

        #region Commands
        private async void Close_Cliked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);          
        }

        private async void Accept_Cliked(object sender, EventArgs e)
        {
          if (string.IsNullOrEmpty(PAddressLabel.Text))
          {
              await Application.Current.MainPage.DisplayAlert(
                  Languages.Error,
                  Languages.AddressValidation,
                  Languages.Accept);
              return;
          }

            if (string.IsNullOrEmpty(PLocationLabel.Text))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LocationValidation,
                    Languages.Accept);
                return;
            }

            int n;
            var isNumeric = int.TryParse(PCodePostalLabel.Text, out n);

            if ( !isNumeric)
            {
                await Application.Current.MainPage.DisplayAlert(
                 Languages.Error,
                 Languages.CodePostalValidation,
                 Languages.Accept);
                return;
            }

            int nn;
            var isNumeric2 = int.TryParse(PDoorLabel.Text, out nn);

            if (!isNumeric2)
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                     Languages.DoorValidation,
                     Languages.Accept);
                return;
            }
        
            var _code = PCodePostalLabel.Text;
            var _door = PDoorLabel.Text;
            string code = _code.ToString();
            string door = _door.ToString();

            string infoPlace = PAddressLabel.Text + "\n"
                               + code + "-" + door + "\n"
                               + PLocationLabel.Text;

            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(infoPlace));
            await PopupNavigation.Instance.PopAsync(true);

            #endregion
        }
    }
}