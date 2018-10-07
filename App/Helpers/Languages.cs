using System.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using smileRed21.Resources;
using smileRed21.Interfaces;

namespace smileRed21.Helpers
{
    
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }

        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }

        public static string PasswordValidation
        {
            get { return Resource.PasswordValidation; }
        }

        public static string SomethingWrong
        {
            get { return Resource.SomethingWrong; }
        }

        public static string Login
        {
            get { return Resource.Login; }
        }

        public static string EMail
        {
            get { return Resource.EMail; }
        }

        public static string Password
        {
            get { return Resource.Password; }
        }

        public static string Forgot
        {
            get { return Resource.Forgot; }
        }

        public static string Register
        {
            get { return Resource.Register; }
        }

        public static string Search
        {
            get { return Resource.Search; }
        }
      
        public static string MyLanguages
        {
            get { return Resource.MyLanguages; }
        }

        public static string Menu
        {
            get { return Resource.Menu; }
        }

        public static string MyProfile
        {
            get { return Resource.MyProfile; }
        }

        public static string Statics
        {
            get { return Resource.Statics; }
        }

        public static string LogOut
        {
            get { return Resource.LogOut; }
        }
        public static string RegisterTitle
        {
            get { return Resource.RegisterTitle; }
        }

        public static string FirstNameLabel
        {
            get { return Resource.FirstNameLabel; }
        }

        public static string FirstNameValidation
        {
            get { return Resource.FirstNameValidation; }
        }

        public static string LastNameLabel
        {
            get { return Resource.LastNameLabel; }
        }

        public static string LastNameValidation
        {
            get { return Resource.LastNameValidation; }
        }

        public static string PhoneLabel
        {
            get { return Resource.PhoneLabel; }
        }

        public static string PhoneValidation
        {
            get { return Resource.PhoneValidation; }
        }

        public static string ConfirmLabel
        {
            get { return Resource.ConfirmLabel; }
        }

        public static string ConfirmValidation
        {
            get { return Resource.ConfirmValidation; }
        }

        public static string EmailValidation2
        {
            get { return Resource.EmailValidation2; }
        }

        public static string EmailExists
        {
            get { return Resource.EmailExists; }
        }
        
        public static string PasswordValidation2
        {
            get { return Resource.PasswordValidation2; }
        }

        public static string ConfirmValidation2
        {
            get { return Resource.ConfirmValidation2; }
        }

        public static string UserRegisteredMessage
        {
            get { return Resource.UserRegisteredMessage; }
        }

        public static string UserRegistered
        {
            get { return Resource.UserRegistered; }
        }

        public static string Cancel
        {
            get { return Resource.Cancel; }
        }

        public static string Save
        {
            get { return Resource.Save; }
        }

        public static string ChangePassword
        {
            get { return Resource.ChangePassword; }
        }

        public static string CurrentPassword
        {
            get { return Resource.CurrentPassword; }
        }

        public static string NewPassword
        {
            get { return Resource.NewPassword; }
        }

        public static string ConnectionError1
        {
            get { return Resource.ConnectionError1; }
        }

        public static string ConnectionError2
        {
            get { return Resource.ConnectionError2; }
        }

        public static string LoginError
        {
            get { return Resource.LoginError; }
        }

        public static string ChagePasswordConfirm
        {
            get { return Resource.ChagePasswordConfirm; }
        }

        public static string PasswordError
        {
            get { return Resource.PasswordError; }
        }

        public static string ErrorChangingPassword
        {
            get { return Resource.ErrorChangingPassword; }
        }

        public static string AddressLabel
        {
            get { return Resource.AddressLabel; }
        }

        public static string AddressValidation
        {
            get { return Resource.AddressValidation; }
        }

        public static string LocationLabel
        {
            get { return Resource.LocationLabel; }
        }

        public static string LocationValidation
        {
            get { return Resource.LocationValidation; }
        }

        public static string CodePostalLabel
        {
            get { return Resource.CodePostalLabel; }
        }

        public static string CodePostalValidation
        {
            get { return Resource.CodePostalValidation; }
        }

        public static string DoorLabel
        {
            get { return Resource.DoorLabel; }
        }

        public static string DoorValidation
        {
            get { return Resource.DoorValidation; }
        }

        public static string Register2
        {
            get { return Resource.Register2; }
        }

        public static string Enter
        {
            get { return Resource.Enter; }
        }

        public static string Help
        {
            get { return Resource.Help; }
        }

        public static string Ok
        {
            get { return Resource.Ok; }
        }

        public static string Register3
        {
            get { return Resource.Register3; }
        }

        public static string DeliveryLabel
        {
            get { return Resource.DeliveryLabel; }
        }

        public static string GeolocationLabel
        {
            get { return Resource.GeolocationLabel; }
        }

        public static string Or
        {
            get { return Resource.Or; }
        }
  
        public static string YouAreHere
        {
            get { return Resource.YouAreHere; }
        }
      
        public static string CustomDetailInfo
        {
            get { return Resource.CustomDetailInfo; }
        }

        public static string GpsFailure
        {
            get { return Resource.GpsFailure; }
        }

        public static string UbicationLabel
        {
            get { return Resource.UbicationLabel; }
        }

        public static string AddressGeolocationValidation
        {
            get { return Resource.AddressGeolocationValidation; }
        }

        public static string EnterDataLabel
        {
            get { return Resource.EnterDataLabel; }
        }
        public static string CloseLabel
        {
            get { return Resource.CloseLabel; }
        }
      
            public static string PleaseWait
        {
            get { return Resource.PleaseWait; }
        }

        public static string LoginLabel
        {
            get { return Resource.LoginLabel; }
        }

        public static string HomeLabel
        {
            get { return Resource.HomeLabel; }
        }

        public static string AddressReference
        {
            get { return Resource.AddressReference; }
        }

        public static string SourceImageQuestion
        {
            get { return Resource.SourceImageQuestion; }
        }

        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }

        public static string FromCamera
        {
            get { return Resource.FromCamera; }
        }

        public static string ProfileLabel
        {
            get { return Resource.ProfileLabel; }
        }

        public static string FavoritesLabel
        {
            get { return Resource.FavoritesLabel; }
        }

        public static string CartLabel
        {
            get { return Resource.CartLabel; }
        }

        public static string NotificationsLabel
        {
            get { return Resource.NotificationsLabel; }
        }

        public static string ContactLabel
        {
            get { return Resource.ContactLabel; }
        }

        public static string HoraryLabel
        {
            get { return Resource.HoraryLabel; }
        }

        public static string ReservationLabel
        {
            get { return Resource.ReservationLabel; }
        }

        public static string AddressRefLabel
        {
            get { return Resource.AddressRefLabel; }
        }
       
        public static string CurrentPasswordValidation
        {
            get { return Resource.CurrentPasswordValidation; }
        }

        public static string NewPasswordValidation
        {
            get { return Resource.NewPasswordValidation; }
        }

        public static string CurrentPasswordValidation2
        {
            get { return Resource.CurrentPasswordValidation2; }
        }

        public static string NewPasswordValidation2
        {
            get { return Resource.NewPasswordValidation2; }
        }


        public static string PasswordRecoveryLabel
        {
            get { return Resource.PasswordRecoveryLabel; }
        }

        public static string RemenberLabel
        {
            get { return Resource.RemenberLabel; }
        }

        public static string PasswordRecoveryTitle
        {
            get { return Resource.PasswordRecoveryTitle; }
        }

        public static string NotSentEmailLabel
        {
            get { return Resource.NotSentEmailLabel; }
        }

        public static string SentEmailLabel
        {
            get { return Resource.SentEmailLabel; }
        }

        public static string ProductsLabel
        {
            get { return Resource.ProductsLabel; }
        }

        public static string MapLabel
        {
            get { return Resource.MapLabel; }
        }
         
        public static string QtaLabel
        {
            get { return Resource.QtaLabel; }
        }

        public static string IngreLabel
        {
            get { return Resource.IngreLabel; }
        }
        
        public static string QuantityValidation
        {
            get { return Resource.QuantityValidation; }
        }

        public static string OrderLabel
        {
            get { return Resource.OrderLabel; }
        }

        public static string IngredientSuggestionPlaceHolder
        {
            get { return Resource.IngredientSuggestionPlaceHolder; }
        }
           
         public static string YouWantLabel
        {
            get { return Resource.YouWantLabel; }
        }

        public static string NextOrderLabel
        {
            get { return Resource.NextOrderLabel; }
        }

        public static string CloseOrderLabel
        {
            get { return Resource.CloseOrderLabel; }
        }
    }
}
