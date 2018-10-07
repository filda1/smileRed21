using Rg.Plugins.Popup.Pages;
using smileRed21.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed21.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditNewAddressPage : PopupPage
    {
		public EditNewAddressPage ()
		{
			InitializeComponent ();
            if (!string.IsNullOrEmpty(Settings.NewAddress))
            {
               Entry1.Text = Settings.NewAddress;              
            }
        }
	}
}