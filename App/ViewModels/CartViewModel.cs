using GalaSoft.MvvmLight.Command;
using smileRed21.Domain;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed21.ViewModels
{
   public class CartViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attibrutes
        private ObservableCollection<Order> cart;
        private bool isRefreshing;
        private int orderID;
        private string countOrderID;
        private string iconCart;
        //private Order order;
        #endregion

        #region Properties
        public ObservableCollection<Order> Cart
        {
            get { return this.cart; }
            set { SetValue(ref this.cart, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public int OrderID
        {
            get { return this.orderID; }
            set { SetValue(ref this.orderID, value); }
        }

        public string CountOrderID
        {
            get { return this.countOrderID; }
            set { SetValue(ref this.countOrderID, value); }
        }

        public string IconCart
        {
            get { return this.iconCart; }
            set { SetValue(ref this.iconCart, value); }
        }

        public UserLocal User
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public CartViewModel()
        {
            this.User = MainViewModel.GetInstance().User;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadOrders();        
        }
        #endregion

        #region Methods
        private async void LoadOrders()
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
            var response = await this.apiService.GetList<Order>(
            apiSecurity,
            "/api",
            "/Orders1/GetOrderID",
            MainViewModel.GetInstance().Token.TokenType,
            MainViewModel.GetInstance().Token.AccessToken,
            User.UserId);

            var list= (List<Order>)response.Result;
            this.Cart = new ObservableCollection<Order>(list);

           // Settings.IsCountOrderID = this.Cart.Count.ToString();

            this.IsRefreshing = false;
        }
        #endregion

        #region SubMethods
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadOrders);
            }
        }
        #endregion
    }
}
