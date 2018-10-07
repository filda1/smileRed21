using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using smileRed21.Domain;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed21.ViewModels
{
    public class ModifyAddressViewModel : BaseViewModel
    {
       #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        private string address;
        private ImageSource imageSource;
        private MediaFile file;
        #endregion

        #region Properties 
        public ImageSource ImageSource
        {
                get { return this.imageSource; }
                set { SetValue(ref this.imageSource, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public OrderDetails OrderDetails
        {
            get;
            set;
        }

        public int UserID
        {
            get;
            set;
        }

        public Order Order
        {
            get;
            set;
        }

        public int OrderDetailsID
        {
            get;
            set;
        }


        public int OrderID
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

        public bool VisibleOrderDetails
        {
            get;
            set;
        }

        public bool ActiveOrderDetails
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

        public string Address
        {
            get { return this.address; }
            set { SetValue(ref this.address, value); }
        }

        public UserLocal User
        {
            get;
            set;
        }

        public ProductsOrders ProductsOrders
        {
            get;
            set;
        }

        public int ID_Order
        {
            get;
            set;
        }

        public int N_Order
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public ModifyAddressViewModel(ProductsOrders po)
        {
            this.ProductsOrders = po;
            this.OrderID = po.OrderID;
            this.DateOrder = po.DateOrder;
        
            this.User = MainViewModel.GetInstance().User;        
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.IsEnabled = true;
            this.ImageSource = this.User.ImagePath;       
        }
        #endregion

        #region Commands
        public ICommand EditNewAddressCommand
        {
            get
            {
                return new RelayCommand(EditNewAddress);
            }
        }

        public ICommand MapsNewAddressCommand
        {
            get
            {
                return new RelayCommand(MapsNewAddress);
            }
        }

        public ICommand CloseNewAddressCommand
        {
            get
            {
                return new RelayCommand(CloseNewAddress);
            }
        }

        public ICommand SendOrderCommand
        {
            get
            {
                return new RelayCommand(SendOrder);
            }
        }
        #endregion

        #region Methods
      
            async  void EditNewAddress()
        {
            await PopupNavigation.Instance.PopAsync(true);
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.EditNewAddress = new EditNewAddressViewModel(this.ProductsOrders);
            await PopupNavigation.Instance.PushAsync(new EditNewAddressPage());
        }

        async void MapsNewAddress()
        {
            await PopupNavigation.Instance.PopAsync(true);
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.UbicationsNewAddress= new UbicationsNewAddressViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new UbicationsNewAddress(this.ProductsOrders));
        }

     private async void SendOrder()
        {          

          if (string.IsNullOrEmpty(Settings.ActiveAddress))
          {               
            var checkConnetion2 = await this.apiService.CheckConnection();
            if (!checkConnetion2.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion2.Message,
                    Languages.Accept);
                return;
            }

            Order orderN = new Order
            {
                OrderID = this.OrderID,
                OrderStatusID = 2,
                UserId = this.User.UserId,
                Email = this.User.Email,
                DateOrder = this.DateOrder,
                Delete = true,
                VisibleOrders = true,
                ActiveOrders = true,
            };

            var id = Convert.ToString(this.OrderID);
            var tokenType = MainViewModel.GetInstance().Token.TokenType;
            var accessToken = MainViewModel.GetInstance().Token.AccessToken;
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var url = string.Concat(apiSecurity, "/api/Orders1/");

            /**************************** PUT api *************************************/
            var json = JsonConvert.SerializeObject(orderN);
            var content = new StringContent(
                 json,
                 Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
            var response = await client.PutAsync(string.Concat(url, id), content);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert(
                      Languages.Error,
                      "Obrigada, sua Orden ja vai ser enviada",
                      Languages.Accept);
                return;
            }

            /****************************************************************************/

                await PopupNavigation.Instance.PopAsync(true);
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Home = new MasterDetailPage1DetailViewModel();
                Application.Current.MainPage = new MasterDetailPage1();
          }

          else
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
                      "Obrigada, sua Ordem ja vai ser procesada",
                      Languages.Accept); ;
                    return;
                }

                await PopupNavigation.Instance.PopAsync(true);
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Home = new MasterDetailPage1DetailViewModel();
                Application.Current.MainPage = new MasterDetailPage1();
            }                
        }
        #region Methods
       

        #endregion

        private async void CloseNewAddress()
        {
            await PopupNavigation.Instance.PopAsync(true);    
        }
        #endregion 
    }
}
