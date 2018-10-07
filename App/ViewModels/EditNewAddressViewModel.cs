using GalaSoft.MvvmLight.Command;
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
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed21.ViewModels
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

            var charater = this.User.Address;
         
            await PopupNavigation.Instance.PopAsync(true);        
            var mainViewModel = MainViewModel.GetInstance();
         
            mainViewModel.ModifyAddress = new ModifyAddressViewModel(this.ProductsOrders);
            await PopupNavigation.Instance.PushAsync(new ModifyAddressPage());          
        }
        private async void CancelNewAddress()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }
        #endregion
    }
}
