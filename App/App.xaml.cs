using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.ViewModels;
using smileRed21.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace smileRed21
{
    public partial class App : Application
    {
        #region Contructors
        public App()
        {
            InitializeComponent();

            if (Settings.IsRemembered == "true")
            {
                Settings.IsPowerON = "true";
                var dataService = new DataService();
                var token = dataService.First<TokenResponse>(false);

                if (token != null && token.Expires > DateTime.Now)
                {
                    Settings.IsLogin = "true";
                    var user = dataService.First<UserLocal>(false);
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.User = user;
                    mainViewModel.Home = new MasterDetailPage1DetailViewModel();
                    Application.Current.MainPage = new MasterDetailPage1();
                }
                else
                {
                  this.MainPage = new NavigationPage(new LoginPage());
                    //this.MainPage = new MasterDetailPage1();
                }
            }

            else
            {
                Settings.IsLogin = "false";
                Settings.IsPowerON = "false";
                //this.MainPage = new NavigationPage(new LoginPage());
                this.MainPage = new MasterDetailPage1();
            }

        #endregion
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
