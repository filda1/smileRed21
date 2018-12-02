using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace smileRed25.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReverseGeocodeNew : PopupPage
    {
        Geocoder geoCoder;
        string infoPlace;

        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        //private  ImageSource imageSource;
        private ImageSource imageSource;
        //private MediaFile file;
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

        public string Direction
        {
            get;
            set;
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

        public ReverseGeocodeNew(GeolocatorService geolocatorServices)
        {
            InitializeComponent();
            geoCoder = new Geocoder();

           // this.ProductsOrders = po;
           // this.OrderID = po.OrderID;
           // this.DateOrder = po.DateOrder;
            var latitude = geolocatorServices.Latitude;
            var longitude = geolocatorServices.Longitude;

            this.User = MainViewModel.GetInstance().User;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            Address_Place();

            async void Address_Place()
            {
                var fortMasonPosition = new Position(latitude, longitude);
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(fortMasonPosition);

                var first = possibleAddresses.First();

                Place.Text = first;
                infoPlace = Place.Text;

                MainViewModel.GetInstance().User.Address = infoPlace;
                this.Direction = infoPlace;
                
                /*foreach (var a in possibleAddresses)
                {
                    //Place.Text += a + "\n";
                    Place.Text += a;
                }
               
                infoPlace = Place.Text;*/


                if (string.IsNullOrEmpty(Place.Text))
                {
                    Caption.Text = Languages.GpsFailure;
                }
                else
                {
                    Caption.Text = Languages.AddressLabel;
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {          
                await PopupNavigation.Instance.PopAsync(true);
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.ModifyAddress = new ModifyAddressViewModel(this.ProductsOrders);
                await PopupNavigation.Instance.PushAsync(new ModifyAddressPage());        
        }

         async void Button_Open(object sender, EventArgs e)
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

           /* var person = new User()
            {
                UserId = this.User.UserId,
                Address = this.Direction,
            };
            var id = Convert.ToString(person.UserId);
            var tokenType = MainViewModel.GetInstance().Token.TokenType;
            var accessToken = MainViewModel.GetInstance().Token.AccessToken;
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var url = string.Concat(apiSecurity, "/api/Users/");

            /**************************** PUT api *************************************/
           /* var json = JsonConvert.SerializeObject(person);
            var content = new StringContent(
                 json,
                 Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
            var response = await client.PutAsync(string.Concat(url, id), content);

          /*  if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert(
                      Languages.Error,
                      "Obrigada, sua Orden ja vai ser enviada",
                      Languages.Accept);
                return;
            }*/

            /****************************************************************************/


               byte[] imageArray = null;
               /*if (this.file != null)
               {
                   imageArray = FilesHelper.ReadFully(this.file.GetStream());
               }*/

               var userDomain = Converter.ToUserDomain(this.User, imageArray);
               var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
               var response = await this.apiService.Put(
                   apiSecurity,
                   "/api",
                   "/Users",
                   MainViewModel.GetInstance().Token.TokenType,
                   MainViewModel.GetInstance().Token.AccessToken,
                   userDomain);

               if (!response.IsSuccess)
               {
                   await Application.Current.MainPage.DisplayAlert(
                       Languages.Error,
                       response.Message,
                       Languages.Accept);
                   return;
               }

            /*  var userApi = await this.apiService.GetUserByEmail(
                  apiSecurity,
                  "/api",
                  "/Users/GetUserByEmail",
                  MainViewModel.GetInstance().Token.TokenType,
                  MainViewModel.GetInstance().Token.AccessToken,
                  this.User.Email);*/

   
            

            
            // await PopupNavigation.Instance.PopAsync(true);

            /* var mainViewModel = MainViewModel.GetInstance();

             mainViewModel.ModifyAddress = new ModifyAddressViewModel(this.ProductsOrders);
             await PopupNavigation.Instance.PushAsync(new ModifyAddressPage());*/


            /**************************************************************************/
       
         /*
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

            var id = Convert.ToString(orderN.OrderID);
            var tokenType = MainViewModel.GetInstance().Token.TokenType;
            var accessToken = MainViewModel.GetInstance().Token.AccessToken;
//            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var url = string.Concat(apiSecurity, "/api/Orders1/");

            /**************************** PUT api *************************************
            var json = JsonConvert.SerializeObject(orderN);
            var content = new StringContent(
                 json,
                 Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
            var response2 = await client.PutAsync(string.Concat(url, id), content);

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
     
        /* // GEOCODE
       * var b2 = new Button { Text = "Geocode '394 Pacific Ave'" };
         b2.Clicked += async (sender, e) => {
             var xamarinAddress = "394 Pacific Ave, San Francisco, California";
             var approximateLocation = await geoCoder.GetPositionsForAddressAsync(xamarinAddress);
             foreach (var p in approximateLocation)
             {
                 l.Text += p.Latitude + ", " + p.Longitude + "\n";
             }
         };*/
    }
}