using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductPage : ContentPage
	{
        #region Properties
        public  NameTitleView NameTitleView
        {
            get;
            set;
        }
        #endregion

        public ProductPage(string nameProduct)
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

            var labelProduct = new NameTitleView
             {
                 NameTitle = nameProduct,
             };

            this.NameTitleView = labelProduct;
            //BindingContext = labelProduct;
            BindingContext = this.NameTitleView;
        }

        public void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var product = e.SelectedItem as Product;
            Product pro = new Product
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.ImageFullPath,
                VAT = product.VAT,
                Stock = product.Stock,
                IsActive = product.IsActive,
                Remarks = product.Remarks,
            };
            var TitlePage = NameTitleView.NameTitle;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.ProductDescription = new ProductDescriptionPageViewModel(pro);
            Navigation.InsertPageBefore(new ProductDescriptionPage(TitlePage), this);
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