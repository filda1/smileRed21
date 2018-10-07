using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed21.ViewModels
{
    public class AddOrderIDViewModel : BaseViewModel
    {
        #region Propperties     
        public int OrderID
        {
            get;
            set;
        }

        public UserLocal User
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public AddOrderIDViewModel(int N_Order)
        {
            this.OrderID = N_Order;
            this.User = MainViewModel.GetInstance().User;       
        }
        #endregion

        #region Commands
        public ICommand AddYesOrderCommand
        {
            get
            {
                return new RelayCommand(AddYesOrder);
            }
        }

        public ICommand AddNoOrderCommand
        {
            get
            {
                return new RelayCommand(AddNoOrder);
            }
        }
        #endregion

        #region Methods
        private async void AddYesOrder()
        {
              var StringN = this.OrderID.ToString();
              Settings.IsCart = "123";
              Settings.AddOrder = StringN;

              await PopupNavigation.Instance.PopAsync(true);
              var mainViewModel = MainViewModel.GetInstance();
              mainViewModel.Home = new MasterDetailPage1DetailViewModel();
              Application.Current.MainPage = new MasterDetailPage1();          
        }

        private async void AddNoOrder()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion
    }
}
