using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed25.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage1Detail: ContentPage
    {

        public MasterDetailPage1Detail()
        {
            InitializeComponent();

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

             if (Settings.IsLogin == "false")
            {
                ToolHome.Icon = "linuxm.png";
            }

        }


        private void ClikedHome(object o, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home = new MasterDetailPage1DetailViewModel();
            Navigation.InsertPageBefore(new MasterDetailPage1Detail(), this);
            Navigation.PopAsync();
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






