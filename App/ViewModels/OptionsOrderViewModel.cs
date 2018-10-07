using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
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
   public class OptionsOrderViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attibrutes
        private ObservableCollection<Order> order;
        private int orders_;
 
        //private Order order;
        #endregion

        #region Properties
        public ObservableCollection<Order> Order1
        {
            get { return this.order; }
            set { SetValue(ref this.order, value); }
        }


        public int ID_Order
        {
            get { return this.orders_; }
            set { SetValue(ref this.orders_, value); }
        }

        public Order Order
        {
            get;
            set;
        }


        public List<Order> OrderList
        {
            get;
            set;
        }

        public string SubTitle
        { 
            get;
            set;
        }
        public int ProductID
        {
            get;
            set;
        }
        public int Quantity
        {
            get;
            set;
        }
        public string Ingredients
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
        public OptionsOrderViewModel(OrderDetails pro)
        {
            this.User = MainViewModel.GetInstance().User;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.ProductID = pro.ProductID;
            this.Quantity = pro.Quantity;
            this.Ingredients = pro.Ingredients;
            this.LoadOrder();
        }
        #endregion

        #region Methods
        private async  void LoadOrder()
        {       
            if (string.IsNullOrEmpty(Settings.AddOrder))
            {
            
                var userId = this.User.UserId;
                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

                var response = await this.apiService.Get<Order>(
                       apiSecurity,
                       "/api",
                       "/Orders1",
                       MainViewModel.GetInstance().Token.TokenType,
                       MainViewModel.GetInstance().Token.AccessToken,
                       userId);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        response.Message,
                        Languages.Accept);
                    return;
                }

                var bar = (Order)response.Result;
                this.ID_Order = bar.OrderID;

                /*string N_Order = this.ID_Order.ToString();
                Settings.IsCart = N_Order;*/
                Settings.IsCart = "";

            }
            else
            {
                var N = Settings.AddOrder;
                int N_Order = Convert.ToInt32(N);

                this.ID_Order = N_Order;              
            }           
        }
        #endregion

        #region Commands
        public ICommand NextOrderCommand
        {
            get
            {
                return new RelayCommand(NextOrder);
            }
        }
        private async void NextOrder()
        {
            //var IsCart = Convert.ToInt32(Settings.IsCart);
       
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var orderDetails = new OrderDetails
             {
                  OrderID = this.ID_Order,
                  ProductID = this.ProductID,
                  Quantity = this.Quantity,
                  VisibleOrderDetails = true,
                  ActiveOrderDetails = true,
                  Ingredients = this.Ingredients,
              };

              var MyOrderDetais = await this.apiService.Post(
                  apiSecurity,
                  "/api",
                  "/OrderDetails",
                  MainViewModel.GetInstance().Token.TokenType,
                  MainViewModel.GetInstance().Token.AccessToken,
                  orderDetails);

                string N_Order = this.ID_Order.ToString();
                Settings.IsCart = N_Order;
          
            Application.Current.MainPage = new MasterDetailPage1();
            await PopupNavigation.Instance.PopAsync(true);
        }
    
        public ICommand CloseOrderCommand
        {
            get
            {
                return new RelayCommand(CloseOrder);
            }
        }

        private async void CloseOrder()
        {
           //var IsCart2 = Convert.ToInt32(Settings.IsCart);
       
                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
                var orderDetails1 = new OrderDetails
                {
                    OrderID = this.ID_Order,
                    ProductID = this.ProductID,
                    Quantity = this.Quantity,
                    VisibleOrderDetails = true,
                    ActiveOrderDetails = true,
                    Ingredients = this.Ingredients,
                };

                var MyOrderDetais1 = await this.apiService.Post(
                    apiSecurity,
                    "/api",
                    "/OrderDetails",
                    MainViewModel.GetInstance().Token.TokenType,
                    MainViewModel.GetInstance().Token.AccessToken,
                    orderDetails1);

                Settings.IsCart = "";
                Settings.AddOrder = "";

            Application.Current.MainPage = new MasterDetailPage1();
            await PopupNavigation.Instance.PopAsync(true);

            /* else
             {
                 Settings.IsCart = "";
                 await PopupNavigation.Instance.PopAsync(true);
             }*/
        }
        #endregion

   }
}
