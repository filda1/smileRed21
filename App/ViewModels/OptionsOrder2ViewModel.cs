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
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed21.ViewModels
{
    public class OptionsOrder2ViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attibrutes
        private ObservableCollection<Order> order;
        private int orders_;
        private int lastOrderID;
        private int orderStatusID;

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

        public int LastOrderID
        {
            get { return this.lastOrderID; }
            set { SetValue(ref this.lastOrderID, value); }
        }

        public int OrderStatusID
        {
            get { return this.orderStatusID; }
            set { SetValue(ref this.orderStatusID, value); }
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

        public DateTime DateOrder
        {
            get;
            set;
        }
        public UserLocal User
        {
            get;
            set;
        }

        public ObservableCollection<Order> Cart1
        {
            get;
            set;
        }
        #endregion

        #region Contructors
        public OptionsOrder2ViewModel(ProductsOrders pro)
        {
            this.User = MainViewModel.GetInstance().User;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.DateOrder = pro.DateOrder;
            this.ProductID = pro.ProductId;
            this.Quantity = this.Quantity;
            this.Ingredients = pro.Ingredients;

            //this.LoadOrder();
        }
        #endregion

        #region Methods
        private async void LoadOrder()
        {         
                var userId = this.User.UserId;
                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

                var response = await this.apiService.Get<Order>(
                       apiSecurity,
                       "/api",
                       "/Orders1/GetOrderStatusID",
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
              this.LastOrderID = bar.OrderStatusID;
              var x = bar.OrderID ;

              this.ID_Order = bar.OrderID;

        }
        #endregion

        #region Commands
        public ICommand EditNewAddress2Command
        {
            get
            {
                return new RelayCommand(EditNewAddress2);
            }
        }

        public ICommand MapsNewAddress2Command
        {
            get
            {
                return new RelayCommand(MapsNewAddress2);
            }
        }


        public ICommand SendOrder2Command
        {
            get
            {
                return new RelayCommand(SendOrder2);
            }
        }

        async void EditNewAddress2()
        {
            await PopupNavigation.Instance.PopAsync(true);
          /*  var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditNewAddress = new EditNewAddressViewModel(this.ProductsOrders);
            await PopupNavigation.Instance.PushAsync(new EditNewAddressPage());*/
        }

        async void MapsNewAddress2()
        {
          /*  await PopupNavigation.Instance.PopAsync(true);
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.UbicationsNewAddress = new UbicationsNewAddressViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new UbicationsNewAddress(this.ProductsOrders));*/
        }

        private async void SendOrder2()
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
            ////////////////// POST order ///////////////////

            Order order1 = new Order
            {
                OrderStatusID = 2,
                UserId = this.User.UserId,
                Email = this.User.Email,
                DateOrder = this.DateOrder,
                Delete = true,
                VisibleOrders = true,
                ActiveOrders = true,
            };
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Post(
                    apiSecurity,
                    "/api",
                    "/Orders1",
                    MainViewModel.GetInstance().Token.TokenType,
                    MainViewModel.GetInstance().Token.AccessToken,
                    order1);

            //////////////// LAST GET orderID ///////////////
           
            var response2 = await this.apiService.Get<Order>(
                      apiSecurity,
                      "/api",
                      "/Orders1",
                      MainViewModel.GetInstance().Token.TokenType,
                      MainViewModel.GetInstance().Token.AccessToken,
                      this.User.UserId);


            var bar = (Order)response.Result;
            this.ID_Order = bar.OrderID;

            ////////////// POST orderDetails ///////////////

            var OrderDe = new OrderDetails
            {
                OrderID = this.ID_Order,
                ProductID = this.ProductID,
                Quantity = this.Quantity,
                VisibleOrderDetails = true,
                ActiveOrderDetails = true,
                Ingredients = this.Ingredients,
            };

            var responseOrder = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/OrderDetails",
                MainViewModel.GetInstance().Token.TokenType,
                MainViewModel.GetInstance().Token.AccessToken,
                OrderDe);

            if (!responseOrder.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    responseOrder.Message,
                    Languages.Accept);
                return;
            }

            await PopupNavigation.Instance.PopAsync(true);
            Application.Current.MainPage = new MasterDetailPage1();           
        }


        public ICommand CloseOrder2Command
        {
            get
            {
                return new RelayCommand(CloseOrder2);
            }
        }

        private async void CloseOrder2()
        {          
            await PopupNavigation.Instance.PopAsync(true);  
        }
        #endregion

    }
}
