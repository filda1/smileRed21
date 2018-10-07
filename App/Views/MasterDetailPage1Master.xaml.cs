using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.ViewModels;
//using smileRed21.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed21.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Master : ContentPage
    {
        public ListView ListView;

        public MasterDetailPage1Master()
        {
            InitializeComponent();
            BindingContext = new MasterDetailPage1MasterViewModel();
            ListView = MenuItemsListView;
            
        }

        class MasterDetailPage1MasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPage1MenuItem> MenuItems { get; set; }
            
           
            public MasterDetailPage1MasterViewModel()
            {
              if (Settings.IsLogin == "true")
            {
                    MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>(new[]
                   {
                    new MasterDetailPage1MenuItem { Id = 0, Title = Languages.HomeLabel, Icon = "Dashboard.png",  TargetType = typeof(MasterDetailPage1Detail) },
                    new MasterDetailPage1MenuItem { Id = 1, Title = Languages.ProfileLabel, Icon = "Settings.png", TargetType = typeof(ProfilePage) },
                    new MasterDetailPage1MenuItem { Id = 2, Title = Languages.FavoritesLabel, Icon = "Favorite.png", TargetType = typeof(MasterDetailPage1Detail) },
                    new MasterDetailPage1MenuItem { Id = 3, Title =  Languages.CartLabel, Icon = "car.png",TargetType = typeof(CartPage)},
                    new MasterDetailPage1MenuItem { Id = 4, Title = Languages.NotificationsLabel, Icon = "Flag.png", TargetType = typeof(MasterDetailPage1Detail) },
                   });
             }
                else
                {
                    MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>(new[]
                  {
                    new MasterDetailPage1MenuItem { Id = 0, Title = Languages.HomeLabel, Icon = "Dashboard.png",  TargetType = typeof(MasterDetailPage1Detail) },
                    new MasterDetailPage1MenuItem { Id = 1, Title = Languages.ProfileLabel, Icon = "Settings.png", TargetType = typeof(LoginPage)},
                    new MasterDetailPage1MenuItem { Id = 2, Title = Languages.FavoritesLabel, Icon = "Favorite.png", TargetType = typeof(LoginPage)},
                    new MasterDetailPage1MenuItem { Id = 3, Title = Languages.CartLabel, Icon = "car.png", TargetType = typeof(LoginPage)},
                    new MasterDetailPage1MenuItem { Id = 4, Title = Languages.NotificationsLabel, Icon = "Flag.png", TargetType = typeof(LoginPage)},
                   });
                }
            
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

      /*  private void Tap_Power(object sender, EventArgs e)
        {
            Settings.IsLogin = "false";

            if (Settings.IsPowerON == "true")
            {
                Settings.IsRemembered = "false";
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = null;
                mainViewModel.User = null;
                Application.Current.MainPage = new NavigationPage(
                new LoginPage());
            }
            else
            {
                Settings.IsRemembered = "true";
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = null;
                mainViewModel.User = null;
                Application.Current.MainPage = new NavigationPage(
                new LoginPage());
            }
        }*/
    }
}