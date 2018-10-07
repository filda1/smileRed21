using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using smileRed21.Domain;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed21.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModifyAddressPage : PopupPage
    {       
        public ModifyAddressPage ()
		{
			InitializeComponent ();
           // PAddressLabel.Text = character;
        }
    }
}