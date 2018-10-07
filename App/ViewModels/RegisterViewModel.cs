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
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace smileRed21.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        #region Services
        private readonly ApiService apiService;
        //private  string location;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
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

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Telephone
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Confirm
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public int Code
        {
            get;
            set;
        }

        public int Door
        {
            get;
            set;
        }

        public string PlaceLabel
        {
            get;
            set;
        }
        //public Command MapsCommand { get; }
        //public Command AddressCommand { get; }
        #endregion

        #region Constructors
        public RegisterViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            //MapsCommand = new Command(Ubications);
            //AddressCommand = new Command(FullAddress);
            this.ImageSource = "";          
        }
        #endregion

        #region Commands
        public Command AddressCommand
        {
            get
            {
                return new Command(FullAddress);
            }
        }
        private async void FullAddress()
        {
            this.IsEnabled = false;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.FullAddress = new AddressViewModel();
            await PopupNavigation.Instance.PushAsync(new FullAddress());
        }

        public Command MapsCommand
        {
            get
            {
                return new Command(Ubications);
            }
        }
        private async void Ubications()
        {

            this.isRunning = true;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Ubications = new UbicationsView();
            await Application.Current.MainPage.Navigation.PushAsync(new UbicationsView());
            this.isRunning = false;
        }

        public Command RegisterCommand
        {
            get
            {
                return new Command(Register);
            }
        }

        private async void Register()
        {
            if (string.IsNullOrEmpty(this.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirstNameValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastNameValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }


            if (!RegexUtilities.IsValidEmail(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                     Languages.Error,
                     Languages.EmailValidation2,
                     Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Telephone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                return;
            }

            if (this.Password.Length < 6)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation2,
                    Languages.Accept);
                return;

            }

            if (string.IsNullOrEmpty(this.Confirm))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConfirmValidation,
                    Languages.Accept);
                return;
            }

            if (this.Password != this.Confirm)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConfirmValidation2,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.PlaceLabel))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AddressGeolocationValidation,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

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

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            if (this.Location == null)
            {
                 this.Location = "-";
            }

            //REGISTER USERS
             var user = new User
            {
                TypeofUserId = 2,
                Email = this.Email,
                FirstName = this.FirstName, 
                LastName = this.LastName,
                Telephone = this.Telephone,
                ImageArray = imageArray,
                //ImagePath = "",
                //ImageFullPath = "",             
                Password = this.Password,
                Address = this.PlaceLabel,
                Location = this.Location,
                Code = 1,
                Door = 1,
                FullName = this.FirstName + "" + this.LastName,
                Active = true,
               
            };

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Post(
                 apiSecurity,
                 "/api",
                 "/Users",
                 user);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                   Languages.ConfirmLabel,
                   Languages.UserRegisteredMessage,
                   Languages.Accept);

            this.PlaceLabel = "";
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        public Command ChangeImageCommand
        {
            get
            {
                return new Command(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await Application.Current.MainPage.DisplayActionSheet(
                    Languages.SourceImageQuestion,
                    Languages.Cancel,
                    null,
                    Languages.FromGallery,
                    Languages.FromCamera);

                if (source == Languages.Cancel)
                {
                    this.file = null;
                    return;
                }

                if (source == Languages.FromCamera)
                {
                    this.file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                        }
                    );
                }
                else
                {
                    this.file = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }
        #endregion
    }
}