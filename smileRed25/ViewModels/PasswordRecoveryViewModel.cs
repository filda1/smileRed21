using GalaSoft.MvvmLight.Command;
using smileRed25.Helpers;
using smileRed25.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed25.ViewModels
{
    public class PasswordRecoveryViewModel: BaseViewModel
    {      
        #region Services
        ApiService apiService;
        DialogService dialogService;
       // NavigationService navigationService;
        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        //private ImageSource imageSource;
        //private MediaFile file;
        #endregion

        #region Properties
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


        public string Email
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public PasswordRecoveryViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            //navigationService = new NavigationService();

            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        async void Save()
        {

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

            IsRunning = true;
            IsEnabled = false;

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
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            var response = await apiService.PasswordRecovery(
                apiSecurity,
                "/api",
                "/Users/PasswordRecovery",
                Email);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    Languages.Error,
                    Languages.NotSentEmailLabel);
                return;
            }

            await dialogService.ShowMessage(
                Languages.ConfirmLabel,
                Languages.SentEmailLabel);
           // await navigationService.BackOnLogin();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
