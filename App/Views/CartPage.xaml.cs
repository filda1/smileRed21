using smileRed21.Domain;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed21.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartPage : ContentPage
	{    
        public CartPage ()
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

            /* var name = Languages.CartLabel + " " + "" + "(" + Settings.IsCountOrderID + ")";
             var labelTitle = new NameTitleView
             {
                 NameTitle = name,
             };

             BindingContext = labelTitle;*/

        
        }

        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var or = e.SelectedItem as Order;
            Order order = new Order
            {
               OrderID = or.OrderID,
               OrderStatusID = or.OrderStatusID,
               UserId = or.UserId,
               Email = or.Email,
               Delete = or.Delete,
               DateOrder = or.DateOrder,
               VisibleOrders = or.VisibleOrders,
               ActiveOrders = or.ActiveOrders,
            };
            string address = "";
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.CartDescription = new CartDescriptionViewModel(order);
            Navigation.InsertPageBefore(new CartPageDescription(order,address), this);
            Navigation.PopAsync();
        }

        private void ClikedBarCart(object o, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Cart = new CartViewModel();
            Navigation.InsertPageBefore(new CartPage(), this);
            Navigation.PopAsync();
        }
    }
}