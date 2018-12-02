using Rg.Plugins.Popup.Services;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed25.Views
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartPageDescription : ContentPage
    {
        public int OrderID
        {
            get;
            set;
        }
    
        public CartPageDescription (Order order,string address)
		{
			InitializeComponent ();
            this.OrderID = order.OrderID;
          
            T1.Text = "x";
            T2.Text = "Add";
        }

        public async  void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var or = e.SelectedItem as ProductsOrders;
            ProductsOrders orderP = new ProductsOrders
            {
                OrderID = or.OrderID,

                Quantity = or.Quantity,
                Description = or.Description,
                VAT = or.VAT,
                Price = or.Price, 
                Ingredients = or.Ingredients,
                OrderDetailsID = or.OrderDetailsID,
                Remarks = or.Remarks,
            };

          
           var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ModifyOrDeleteOrderDetailsID = new ModifyOrDeleteOrderDetailsIDViewModel(orderP);
            await PopupNavigation.Instance.PushAsync(new ModifyOrDeleteOrderDetailsIDPage());
  
        }

        private async void ClikedBarCart2(object o, EventArgs e)
        {         
           var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.AddOrderID = new AddOrderIDViewModel(this.OrderID);
            await PopupNavigation.Instance.PushAsync(new AddOrderIDPage());
        }

        private async void ClikedBarCart(object o, EventArgs e)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.DeleteOrderID = new DeleteOrderIDViewModel(this.OrderID);
            await PopupNavigation.Instance.PushAsync(new DeleteOrderIDPage());
        }
    }
}