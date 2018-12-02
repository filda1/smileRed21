namespace smileRed25.Views
{
    using System;
    using System.Threading.Tasks;
    using Rg.Plugins.Popup.Services;
    using Services;
    using smileRed25.Helpers;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
  
    public partial class UbicationsView : ContentPage
    {
        #region Services
        GeolocatorService geolocatorService;
        #endregion

        #region Constructors
        public UbicationsView()
        {
            InitializeComponent();

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
            if (Settings.IsLogin == "false")
            {
                //await Navigation.PushAsync(new ReverseGeocode());
                await PopupNavigation.Instance.PushAsync(new ReverseGeocode1(geolocatorService));
            }
            if (Settings.IsLogin == "true")
            {
                await PopupNavigation.Instance.PushAsync(new ReverseGeocodeNew(geolocatorService));
            }

        }
        #endregion
    }
 
}
