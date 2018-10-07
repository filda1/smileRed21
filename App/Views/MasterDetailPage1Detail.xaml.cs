using smileRed21.Domain;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed21.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail: ContentPage
    {

        public MasterDetailPage1Detail()
        {
            InitializeComponent();

           /* if ((String.IsNullOrEmpty(Settings.IsCountOrderID)) || (Settings.IsCountOrderID == "0"))
            {
                ToolOrderID.Text = "";
                ToolIcon.Icon = "";
            }
            else
            {
                ToolOrderID.Text = "(" + Settings.IsCountOrderID + ")";
                ToolIcon.Icon = "car.png";
            }*/

            this.BindingContext = new MyCountOrderViewModel();
       
        }

        public class MyCountOrderViewModel : BaseViewModel
        {
            #region Services
            private readonly ApiService apiService;
            private readonly DataService dataService;
            #endregion

            #region Attributes
            private bool isRefreshing;
            #endregion

            #region Propperties 

            public bool IsRefreshing
            {
                get { return this.isRefreshing; }
                set { SetValue(ref this.isRefreshing, value); }
            }

            public string CountOrdeID
            {
                get;
                set;
            }

            public string IconCart
            {
                get;
                set;
            }
            public UserLocal User
            {
                get;
                set;
            }

            public ObservableCollection<Order> Cart1
            {
                get;
                set;
            }
            #endregion

            #region Constructors
            public MyCountOrderViewModel()
            {
                this.User = MainViewModel.GetInstance().User;
                this.apiService = new ApiService();
                this.dataService = new DataService();

                this.LoadOrders();

                if ((String.IsNullOrEmpty(Settings.IsCountOrderID)) || (Settings.IsCountOrderID == "0"))
                {
                    this.CountOrdeID = "";
                    this.IconCart = "";
                }
                else
                {
                    this.CountOrdeID = "(" + Settings.IsCountOrderID + ")";
                    this.IconCart = "car.png";
                }

            }
            #endregion
            #region Methods
            private async void LoadOrders()
            {
                this.IsRefreshing = true;
                var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

                // CountOrderID x userID
                var response2 = await this.apiService.GetList<Order>(
                apiSecurity,
                "/api",
                "/Orders1/GetOrderID",
                MainViewModel.GetInstance().Token.TokenType,
                MainViewModel.GetInstance().Token.AccessToken,
                User.UserId);

                var list = (List<Order>)response2.Result;
                this.Cart1 = new ObservableCollection<Order>(list);

                Settings.IsCountOrderID = this.Cart1.Count.ToString();

                this.IsRefreshing = false;

            }
            #endregion
        }


        private void ClikedBarCart(object o, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Cart = new CartViewModel();
            Navigation.InsertPageBefore(new CartPage(), this);
            Navigation.PopAsync();
        }

        private void Tap_Burger(object sender, EventArgs e)
        {
            string nameProduct = "Burgers";

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Product = new ProductViewModel(nameProduct);
            Navigation.InsertPageBefore(new ProductPage(nameProduct), this);
            Navigation.PopAsync();
        }
        private void Tap_Pizza(object sender, EventArgs e)
        {
            string nameProduct = "Pizzas";

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Product = new ProductViewModel(nameProduct);
            Navigation.InsertPageBefore(new ProductPage(nameProduct), this);
            Navigation.PopAsync();
        }
    }
}






