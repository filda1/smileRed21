namespace smileRed21.Views
{
    using System;
    using System.Threading.Tasks;
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Services;
    using smileRed21.Helpers;
    using smileRed21.Models;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    public partial class UbicationsNewAddress : PopupPage
    {
        public ProductsOrders ProductsOrders
        {
            get;
            set;
        }

        #region Services
        GeolocatorService geolocatorService;
        #endregion

        #region Constructor
        public UbicationsNewAddress (ProductsOrders po)
		{
			InitializeComponent ();

            this.ProductsOrders = po;
            geolocatorService = new GeolocatorService();
            MoveMapToCurrentPosition();
        }
        #endregion

        #region Methods
        async void MoveMapToCurrentPosition()
        {
            await geolocatorService.GetLocation();
            if (geolocatorService.Latitude != 0 ||
                geolocatorService.Longitude != 0)
            {
                var position = new Position(
                    geolocatorService.Latitude,
                    geolocatorService.Longitude);
                MyMaps.MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromKilometers(.5)));
            }

            //Pin
            var position2 = new Position(geolocatorService.Latitude, geolocatorService.Longitude); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position2,
                Label = Languages.YouAreHere,
                Address = Languages.CustomDetailInfo
            };

            MyMaps.Pins.Add(pin);
        }
        #endregion

        #region Commands
        private async void Button_Clicked(object o, EventArgs e)
        {
            //await Navigation.PushAsync(new ReverseGeocode());
            await PopupNavigation.Instance.PushAsync(new ReverseGeocodeNew(geolocatorService, this.ProductsOrders));
        }
        #endregion
    }

}
