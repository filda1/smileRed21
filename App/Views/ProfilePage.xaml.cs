﻿using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed21.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ProfilePage : ContentPage
	{       
        public ProfilePage ()
		{
			InitializeComponent ();

            if ((String.IsNullOrEmpty(Settings.IsCountOrderID)) || (Settings.IsCountOrderID == "0"))
            {
                ToolOrderID.Text = "";
                ToolIcon.Icon = "";
            }
            else
            {
                ToolOrderID.Text = "(" + Settings.IsCountOrderID + ")";
                ToolIcon.Icon = "car.png";
            }


            this.BindingContext = new MyProfileViewModel();
        }

        private void ClikedBarCart(object o, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Cart = new CartViewModel();
            Navigation.InsertPageBefore(new CartPage(), this);
            Navigation.PopAsync();
        }

        public class MyProfileViewModel : BaseViewModel
        {
            #region Services
            private readonly ApiService apiService;
            private readonly DataService dataService;
            #endregion

            #region Attributes
            private bool isRunning;
            private  bool isEnabled;
            //private  ImageSource imageSource;
            private  ImageSource imageSource;
            private MediaFile file;
            //private object Navigation;
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

            public UserLocal User
            {
                get;
                set;
            }
            #endregion
            public MyProfileViewModel()
            {
                this.User = MainViewModel.GetInstance().User;
                this.apiService = new ApiService();
                this.dataService = new DataService();
                this.IsEnabled = true;
                this.ImageSource = this.User.ImagePath;
            }

            #region Commands
            public ICommand ChangeImageCommand
            {
                get
                {
                    return new RelayCommand(ChangeImage);
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

            public ICommand SaveCommand
            {
                get
                {
                    return new RelayCommand(Save);
                }
            }
            async void Save()
            {

            if (string.IsNullOrEmpty(this.User.FirstName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.FirstNameValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.LastName))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.LastNameValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }

            if (!RegexUtilities.IsValidEmail(this.User.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation2,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.Telephone))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PhoneValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.User.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AddressValidation,
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
                this.IsRunning = false;
                this.IsEnabled = true;
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

            this.IsRunning = false;
            this.IsEnabled = true;


                Application.Current.MainPage = new MasterDetailPage1();
                //await App.Navigator.PopAsync();
              
            }

           public ICommand ChangePasswordCommand
            {
                get
                {
                    return new RelayCommand(ChangePassword);
                }
            }


            async void ChangePassword()
            {
                /*MainViewModel.GetInstance().ChangePassword = new ChangePasswordViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new ChangePassword());*/

                //Application.Current.MainPage = new NavigationPage(new ChangePassword());
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.ChangePassword = new ChangePasswordViewModel();
                await PopupNavigation.Instance.PushAsync(new ChangePassword());
            }
        }
        #endregion
      
    }
   
}