using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace smileRed21.Views
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
        private MediaFile file;
        //private object Navigation;
        #endregion

        #region Properties

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

        public ReverseGeocodeNew(GeolocatorService geolocatorServices, ProductsOrders po)
        {
            InitializeComponent();
            geoCoder = new Geocoder();

            this.ProductsOrders = po;
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

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

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

            var userApi = await this.apiService.GetUserByEmail(
                apiSecurity,
                "/api",
                "/Users/GetUserByEmail",
                MainViewModel.GetInstance().Token.TokenType,
                MainViewModel.GetInstance().Token.AccessToken,
                this.User.Email);
            var userLocal = Converter.ToUserLocal(userApi);

            MainViewModel.GetInstance().User = userLocal;
            this.dataService.Update(userLocal);

            await PopupNavigation.Instance.PopAsync(true);
            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.ModifyAddress = new ModifyAddressViewModel(this.ProductsOrders);
            await PopupNavigation.Instance.PushAsync(new ModifyAddressPage());

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