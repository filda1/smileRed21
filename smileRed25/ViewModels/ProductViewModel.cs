using GalaSoft.MvvmLight.Command;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Services;
using smileRed25.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed25.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<ProductItemViewModel> product;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<ProductItemViewModel> Product
        {
            get { return this.product; }
            set { SetValue(ref this.product, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string NameProduct

        { get;
          set;
        }
        #endregion

        #region Constructors
        public ProductViewModel(string nameProduct)
        {
            this.apiService = new ApiService();
            this.NameProduct = nameProduct;
            this.LoadProduct();
        }
        #endregion

        #region Methods
        private async void LoadProduct()
        {
            this.IsRefreshing = true;
            var checkConnetion = await this.apiService.CheckConnection();

            if (!checkConnetion.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }
        
            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.GetList<Product>(
                 apiSecurity,
                 "/api",
                 "/Products/"+ this.NameProduct);
               
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }
            
            MainViewModel.GetInstance().ProductList = (List<Product>)response.Result;
             this.Product = new ObservableCollection<ProductItemViewModel>(
                        this.ToProductItemViewModel());

            this.IsRefreshing = false;
            #endregion           
        }

        #region Methods
        private IEnumerable<ProductItemViewModel> ToProductItemViewModel()
        {
            return MainViewModel.GetInstance().ProductList.Select(l => new ProductItemViewModel
            {
                ProductId = l.ProductId,
                CategoryId = l.CategoryId,
                Name = this.NameProduct + " " +  l.Name,
                Description = l.Description,
                Price  = l.Price,
                VAT = l.VAT,
                Image = l.Image,
                Stock = l.Stock,
                IsActive = l.IsActive,
                Remarks = l.Remarks,
            });
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProduct);
            }
        }
        #endregion
    }
}
