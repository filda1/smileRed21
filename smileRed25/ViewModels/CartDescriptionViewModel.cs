using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed25.ViewModels
{
   public class CartDescriptionViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attibrutes
        private ObservableCollection<ProductsOrders> cartDescription;
        private bool isRefreshing;
        private string priceVATQuantity;
        private string priceQuantity;
        //private Order order;
        #endregion

        #region Properties
        public ObservableCollection<ProductsOrders> CartDescription
        {
            get { return this.cartDescription; ; }
            set { SetValue(ref this.cartDescription, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public int OrderID
        {
            get;
            set;
        }

        public Order Order
        {
            get;
            set;
        }

        public DateTime DateOrder
        {
            get;
            set;
        }
        public TotalRequest TotalRequest
        {
            get;
            set;
        }

        public string PriceVATQuantity
        {
            get { return this.priceVATQuantity; }
            set { SetValue(ref this.priceVATQuantity, value); }
        }
        public string PriceQuantity
        {
            get { return this.priceQuantity; }
            set { SetValue(ref this.priceQuantity, value); }

        }

        public string TotalPrice
        {
            get;
            set;
        }

        public List<ProductsOrders> ListOD
        {
            get;
            set;
        }

        public string Address
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

        #region Contructors
        public CartDescriptionViewModel(Order order)
        {
           // this.Order= MainViewModel.GetInstance().Order;
          
            this.User = MainViewModel.GetInstance().User;
            var x = MainViewModel.GetInstance().User.Address;
            string susti = x.Replace("\n", ",");
            this.Address = susti;

            this.Order = order;
            this.OrderID = order.OrderID;
            this.DateOrder = order.DateOrder;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadOrderDetails();
            this.SUMPriceQuantity();
            this.SUMPriceVATQuantity();
        }
        #endregion

        #region Methods
        private async void LoadOrderDetails()
        {
            this.IsRefreshing = true;
            var checkConnetion = await this.apiService.CheckConnection();

            if (!checkConnetion.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.GetList<ProductsOrders>(
            apiSecurity,
            "/api",
            "/OrderDetails/GetAllOrderDetails",
            MainViewModel.GetInstance().Token.TokenType,
            MainViewModel.GetInstance().Token.AccessToken,
            this.OrderID);

            var list = (List<ProductsOrders>)response.Result;
            this.ListOD = list;
            this.CartDescription = new ObservableCollection<ProductsOrders>(list);
  
            // Settings.IsCountOrderID = this.Cart.Count.ToString();

            this.IsRefreshing = false;
        }

        private async void SUMPriceQuantity()
        {
            var apiSecurity2 = Application.Current.Resources["APISecurity"].ToString();

            var response2 = await this.apiService.Get<TotalRequest>(
                   apiSecurity2,
                   "/api",
                   "/OrderDetails/SumPriceQuantity",
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
        
            var bar = (TotalRequest)response2.Result;
            this.PriceQuantity = bar.PriceQuantity;            
        }

        private async void SUMPriceVATQuantity()
        {         
            var apiSecurity3 = Application.Current.Resources["APISecurity"].ToString();

            var response3 = await this.apiService.Get<TotalRequest>(
                   apiSecurity3,
                   "/api",
                   "/OrderDetails/SumPriceVATQuantity",
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

            var bar = (TotalRequest)response3.Result;
            this.PriceVATQuantity = bar.PriceQuantity;
        }
        #endregion
        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadOrderDetails);
            }
        }

        public ICommand EndOrderCommand
        {
            get
            {
                return new RelayCommand(EndOrder);
            }
        }

        public ICommand ModifyAddressCommand
        {
            get
            {
                return new RelayCommand(ModifyAddress);
            }
        }
        #endregion

        #region SubMethods
        private  async void EndOrder()
        {        
            if(string.IsNullOrEmpty(this.User.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   "Address empty",
                   Languages.Accept);
                Application.Current.MainPage = new ProfilePage(); 
              
            };

            ProductsOrders po = new ProductsOrders
            {
                OrderID = this.OrderID,
                DateOrder = this.DateOrder
            };

            Settings.ActiveAddress = "";

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ModifyAddress = new ModifyAddressViewModel(po);
            await PopupNavigation.Instance.PushAsync(new ModifyAddressPage());
        }

        private void ModifyAddress()
        {
         
        }
        #endregion
    }
}
