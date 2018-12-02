using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.Views;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed25.ViewModels
{
    public class EditNewAddressViewModel: BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        //private  ImageSource imageSource;
        private ImageSource imageSource;
        private MediaFile file;
        //private object Navigation;
        #endregion

        #region Properties
        public int OrderID
        {
            get;
            set;
        }

        public DateTime DateOrder
        {
            get;
            set;
        }

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

        #endregion

        #region Contructors
        public EditNewAddressViewModel(ProductsOrders pro)
        {
            this.ProductsOrders = pro;
            this.User = MainViewModel.GetInstance().User;
            var x = MainViewModel.GetInstance().User.Address;
            Settings.NewAddress = x;

            this.OrderID = pro.OrderID;
            this.DateOrder = pro.DateOrder;
            this.apiService = new ApiService();
            this.dataService = new DataService();         
        }
        #endregion

        #region Comands
        public ICommand OkNewAddressCommand
        {
            get
            {
                return new RelayCommand(OkNewAddress);
            }
        }

        public ICommand CancelNewAddressCommand
        {
            get
            {
                return new RelayCommand(CancelNewAddress);
            }
        }
        #endregion

        #region Methods
        async void OkNewAddress()
        {
            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }
            
            var person = new User()
            {
                UserId = this.User.UserId,
                Email = this.User.Email,
                Address = User.Address,
            };
            var id = Convert.ToString(person.UserId);
            var tokenType = MainViewModel.GetInstance().Token.TokenType;
            var accessToken = MainViewModel.GetInstance().Token.AccessToken;
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var url = string.Concat(apiSecurity, "/api/Users/");

            /**************************** PUT api *************************************/
             var json = JsonConvert.SerializeObject(person);
             var content = new StringContent(
                  json,
                  Encoding.UTF8, "application/json");
             HttpClient client = new HttpClient();
             client.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue(tokenType, accessToken);
             var response = await client.PutAsync(string.Concat(url, id), content);

      
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

            var id1 = Convert.ToString(orderN.OrderID);
            var tokenType1 = MainViewModel.GetInstance().Token.TokenType;
            var accessToken1 = MainViewModel.GetInstance().Token.AccessToken;
            //            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var url1 = string.Concat(apiSecurity, "/api/Orders1/");

            /**************************** PUT api *************************************/
            var json1 = JsonConvert.SerializeObject(orderN);
            var content1 = new StringContent(
                 json1,
                 Encoding.UTF8, "application/json");
            HttpClient client1 = new HttpClient();
            client1.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType1, accessToken1);
            var response2 = await client1.PutAsync(string.Concat(url1, id1), content1);

            if (response2.IsSuccessStatusCode)
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
        private async void CancelNewAddress()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion
    }
}
