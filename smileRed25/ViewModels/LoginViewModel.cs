using smileRed25.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using smileRed25.ViewModels;
using smileRed25.Helpers;
using smileRed25.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using smileRed25.Models;

namespace smileRed25.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;         
        #endregion

        #region Attributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRemembered
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        // public Command LoginCommand { get; }
        // public Command RegisterCommand { get; }
        //public object Navigation { get; private set; }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.Email = "meireles596@Hotmail.com";
            this.Password = "123456";
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.IsRemembered = true;
            this.IsEnabled = true;
            // LoginCommand = new Command(Login);
            //RegisterCommand = new Command(Register); 

        }
        #endregion

        #region Commands
        public ICommand LoginFacebookComand
        {
            get
            {
                return new RelayCommand(LoginFacebook);
            }
        }

        private async void LoginFacebook()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(
                new LoginFacebookPage());

        }


        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
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

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }
        
            //TOKEN
              var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
              var token = await this.apiService.GetToken(
                  apiSecurity,
                  this.Email,
                  this.Password);

              if (token == null)
              {
                  this.IsRunning = false;
                  this.IsEnabled = true;
                  await Application.Current.MainPage.DisplayAlert(
                      Languages.Error,
                      Languages.SomethingWrong,
                      Languages.Accept);
                  return;
              }

              if (string.IsNullOrEmpty(token.AccessToken))
              {
                  this.IsRunning = false;
                  this.IsEnabled = true;
                  await Application.Current.MainPage.DisplayAlert(
                      Languages.Error,
                      Languages.LoginError,
                      Languages.Accept);
                  this.Password = string.Empty;
                  return;
              }

             Settings.IsLogin = "true";

            // GET USER BY EMAIL
             var user = await this.apiService.GetUserByEmail(
             apiSecurity,
             "/api",
             "/Users/GetUserByEmail",
             token.TokenType,
             token.AccessToken,
             this.Email);
          
            var userLocal = Converter.ToUserLocal(user);
            userLocal.Password = this.Password;

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.User = userLocal;

            if (this.IsRemembered)
            {
                Settings.IsRemembered = "true";
            }
            else
            {
               Settings.IsRemembered = "false";
            }

            this.dataService.DeleteAllAndInsert(userLocal);
            this.dataService.DeleteAllAndInsert(token);

            //Settings.Appellation = userLocal.FirstName;
            mainViewModel.Home = new MasterDetailPage1DetailViewModel();
            Application.Current.MainPage = new MasterDetailPage1();           

            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;           
        }

        public ICommand HelpCommand
        {
            get
            {
                return new RelayCommand(Help);
            }
        }

        private async void Help()
        {         
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Helps = new HelpViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new HelpPage());
        }
      
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
         {
            this.IsRunning = true;
            //this.IsEnabled = true;
             string infoPlace = "";
             var mainViewModel = MainViewModel.GetInstance();
             mainViewModel.Register = new RegisterViewModel();
             await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage(infoPlace));
             this.IsRunning = false;
        }
        #endregion
    }
}
