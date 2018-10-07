
using GalaSoft.MvvmLight.Command;
using smileRed21.Domain;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed21.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Attibrutes 
        private UserLocal user;
        private Order order;
        private string imageSource;
        #endregion

        #region Properties
        public TokenResponse Token
        {
            get;
            set;
        }

        public UserLocal User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }

        public string ImageSource
        {
            get { return this.imageSource; }
            set { SetValue(ref this.imageSource, value); }
        }
        public Order Order
        {
            get { return this.order; }
            set { SetValue(ref this.order, value); }
        }
        #endregion

        #region ViewModels
        public MasterDetailPage1DetailViewModel Home
        {
            get;
            set;
        }

        public LoginViewModel Login
        {
            get;
            set;
        }

        public HelpViewModel Helps
        {
            get;
            set;
        }

        public PasswordRecoveryViewModel PasswordRecovery
        {
            get;
            set;
        }

        public RegisterViewModel Register
        {
            get;
            set;
        }

        public UbicationsView Ubications
        {
            get;
            set;
        }

        public AddressViewModel FullAddress
        {
            get;
            set;
        }

        public ProductViewModel Product
        {
            get;
            set;
        }

        public List<Product> ProductList
        {
            get;
            set;
        }

        public ProductDescriptionPageViewModel ProductDescription
        {
            get;
            set;
        }

        public OptionsOrderViewModel OptionsOrder
        {
            get;
            set;
        }

        public OptionsOrder2ViewModel OptionsOrder2
        {
            get;
            set;
        }

        public CartViewModel Cart
        {
            get;
            set;
        }

        public CartDescriptionViewModel CartDescription
        {
            get;
            set;
        }

        public DeleteOrderIDViewModel DeleteOrderID
        {
            get;
            set;
        }

        public AddOrderIDViewModel AddOrderID
        {
            get;
            set;
        }

        public ModifyOrDeleteOrderDetailsIDViewModel ModifyOrDeleteOrderDetailsID
        {
            get;
            set;
        }

        public ModifyAddressViewModel ModifyAddress
        {
            get;
            set;
        }

        public EditNewAddressViewModel EditNewAddress
        {
            get;
            set;
        }

        public UbicationsNewAddressViewModel UbicationsNewAddress
        {
            get;
            set;
        }

        public ChangePasswordViewModel ChangePassword
        {

            get;
            set;
        }

        public ProfileViewModel Profile
        {
            get;
            set;
        }

        /*
           public MenuViewModel Menu
           {
               get;
               set;
           }

           public IngredientsViewModel Ingredients
           {
               get;
               set;
           }


           public EndOrderViewModel EndOrder
           {
               get;
               set;
           }
           */
        #endregion

        #region Constructors

        public MainViewModel()
        {          
            instance = this;
            this.Login = new LoginViewModel();
            //this.Home = new MasterDetailPage1DetailViewModel();
           this.ImageSource = "TurnA.png";
          
                               
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Commands
         public ICommand PowerCommand
        {
            get
            {
                return new RelayCommand(Power);
            }
        }
       private void Power()
        {
            Settings.IsLogin = "false";

            if (Settings.IsPowerON =="true")
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
        }
        #endregion

    }
}


