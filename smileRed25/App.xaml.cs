using Newtonsoft.Json;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.ViewModels;
using smileRed25.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace smileRed25
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

        #region Social network
        public static Action HideLoginView
        {
            get
            {
                return new Action(() => Current.MainPage = new NavigationPage(new LoginPage()));
            }
        }

        public static async Task NavigateToProfile(TokenResponse token)
        {
       
            var apiService = new ApiService();
            var dataService = new DataService();

            if (token == null)
              {
                  Application.Current.MainPage = new NavigationPage(new LoginPage());
                  return;
              }
              var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
              var user = await apiService.GetUserByEmail(
                  apiSecurity,
                  "/api",
                  "/Users/GetUserByEmail",
                  token.TokenType,
                  token.AccessToken,
                  token.UserName);

              UserLocal userLocal = null;
              if (user != null)
              {
                  userLocal = Converter.ToUserLocal(user);
                  dataService.DeleteAllAndInsert(userLocal);
                  dataService.DeleteAllAndInsert(token);
              }

              var mainViewModel = MainViewModel.GetInstance();
              mainViewModel.Token = token;
              mainViewModel.User = userLocal;
              mainViewModel.Home = new MasterDetailPage1DetailViewModel();
              Application.Current.MainPage = new MasterDetailPage1();

              Settings.IsRemembered = "true";
              Settings.IsLogin = "true";
              Settings.IsPowerON = "true";
        }
        #endregion

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
