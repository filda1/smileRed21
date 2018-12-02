using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed25.ViewModels
{
    public class DeleteOrderIDViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attibrutes
        //private Order order;
        #endregion

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
        public DeleteOrderIDViewModel(int N_Order)
        {
            this.OrderID = N_Order;
            this.User = MainViewModel.GetInstance().User;
            this.apiService = new ApiService();
            this.dataService = new DataService();
        }
        #endregion

        #region Commands
        public ICommand DeleteYesOrderCommand
        {
            get
            {
                return new RelayCommand(DeleteYesOrder);
            }
        }

        public ICommand DeleteNoOrderCommand
        {
            get
            {
                return new RelayCommand(DeleteNoOrder);
            }
        }
        #endregion


        #region Methods
        async void DeleteYesOrder()
        {
            var checkConnetion = await this.apiService.CheckConnection();

            if (!checkConnetion.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response2 = await this.apiService.Delete(
                              apiSecurity,
                              "/api",
                              "/Orders1",
                              MainViewModel.GetInstance().Token.TokenType,
                              MainViewModel.GetInstance().Token.AccessToken,
                              this.OrderID);

            if (!response2.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response2.Message,
                    Languages.Accept);
                return;
            }

             var response3 = await this.apiService.Delete(
                            apiSecurity,
                            "/api",
                            "/OrderDetails",
                            MainViewModel.GetInstance().Token.TokenType,
                            MainViewModel.GetInstance().Token.AccessToken,
                            this.OrderID);

             if (!response3.IsSuccess)
             {
                 await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                     response3.Message,
                     Languages.Accept);
                 return;
             }
        
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home = new MasterDetailPage1DetailViewModel();
            Application.Current.MainPage = new MasterDetailPage1();
            await PopupNavigation.Instance.PopAsync(true);
           
            /*var SrtingCount = Settings.IsCountOrderID;
            var IntCount = Convert.ToInt32(SrtingCount);
            var RestCount = IntCount - 1;
            var StringRestCount = RestCount.ToString();
            Settings.IsCountOrderID = StringRestCount;*/
        }

        private async void DeleteNoOrder()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion
    }
}
