using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using smileRed21.Domain;
using smileRed21.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed21.ViewModels
{
    public class ProductItemViewModel: Product
    {
        #region Commands
        public ICommand SelectProductCommand
        {
            get
            {
                return new RelayCommand(SelectProduct);
            }
        }

        private void SelectProduct()
        {
            //var title = "";
            MainViewModel.GetInstance().ProductDescription = new ProductDescriptionPageViewModel(this);
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductDescriptionPage(title));
            //await PopupNavigation.Instance.PopAsync(true);

            //await App.Navigator.PushAsync(new ProductDescriptionPage());
            //await App.Navigator.PopAsync();
        }
        #endregion
    }
}
