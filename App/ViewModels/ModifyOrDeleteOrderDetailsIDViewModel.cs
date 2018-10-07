using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using smileRed21.Domain;
using smileRed21.Helpers;
using smileRed21.Models;
using smileRed21.Services;
using smileRed21.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed21.ViewModels
{
    public class ModifyOrDeleteOrderDetailsIDViewModel : BaseViewModel
    {
        #region Services
        private readonly ApiService apiService;
        private readonly DataService dataService;
        #endregion

        #region Attibrutes 
        private bool isRunning;
        private bool isEnabled;
        private int orderID;
        private int orderDetailsID;
        private float quantity;
        private string ingredients;
        private string description;
        //private Order order;
        #endregion

        #region Properties  
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public int OrderID
        {
            get { return this.orderID; }
            set { SetValue(ref this.orderID, value); }
        }
        public int OrderDetailsID
        {            
               get { return this.orderDetailsID; }
               set { SetValue(ref this.orderDetailsID, value); }            
        }

        public float Quantity
        {
            get { return this.quantity; }
            set { SetValue(ref this.quantity, value); }
        }

        public string Ingredients
        {
            get { return this.ingredients; }
            set { SetValue(ref this.ingredients, value); }
        }
  
        public string Description
        {
            get { return this.description; }
            set { SetValue(ref this.description, value); }
        }

        public UserLocal User
        {
            get;
            set;
        }
        public OrderDetails OrderDetails
        {
            get;
            set;
        }


        #endregion

        #region Contructors
        public ModifyOrDeleteOrderDetailsIDViewModel(ProductsOrders orderP)
        {
            this.User = MainViewModel.GetInstance().User;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.OrderDetailsID =  orderP.OrderDetailsID;
            this.OrderID = orderP.OrderID;
            this.Description = orderP.Description;
            this.Quantity = orderP.Quantity;
            this.Ingredients = orderP.Ingredients;
       

        }
        #endregion
        #region Commands
        public ICommand CancelOrderDetailsIDCommand
        {
            get
            {
                return new RelayCommand(CancelOrderDetailsID);
            }
        }

        /*public ICommand SaveOrdeDetailsIDCommand
        {
            get
            {
                return new RelayCommand(SaveOrdeDetailsID);
            }
        }*/

        public ICommand DeleteProductIDCommand
        {
            get
            {
                return new RelayCommand(DeleteProductID);
            }
        }

        #endregion

        #region Methods
        private async void CancelOrderDetailsID()
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        /* async void SaveOrdeDetailsID()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

            //  Verificacion
            var isNumeric = (int)Math.Round(this.Quantity);

            if (isNumeric == 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                 Languages.Error,
                 Languages.QuantityValidation,
                 Languages.Accept);
                return;
            }

            OrderDetails od = new OrderDetails
            {
                OrderDetailsID = this.OrderDetailsID,
                OrderID = this.OrderID,
                ActiveOrderDetails = false,
                VisibleOrderDetails = false,
                Ingredients = this.Ingredients,
                Quantity = isNumeric,
                ProductID = 9,
            };

            //this.OrderDetails.OrderDetailsID  = this.OrderDetailsID;
           // this.OrderDetails.Quantity = isNumeric;

            //var ODDomain = ConverterOrder.ToOrderDetailsDomain(od);
            var response = await this.apiService.Put(
               apiSecurity,
               "/api",
               "/OrderDetails",
               MainViewModel.GetInstance().Token.TokenType,
               MainViewModel.GetInstance().Token.AccessToken,
               od);

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home = new MasterDetailPage1DetailViewModel();
            Application.Current.MainPage = new MasterDetailPage1();
            await PopupNavigation.Instance.PopAsync(true);

            this.IsRunning = false;
            this.IsEnabled = true;
        }*/

        async void DeleteProductID()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var checkConnetion = await this.apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    checkConnetion.Message,
                    Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response3 = await this.apiService.Delete(
                          apiSecurity,
                          "/api",
                          "/OrderDetails/DeleteOrderDetailsID",
                          MainViewModel.GetInstance().Token.TokenType,
                          MainViewModel.GetInstance().Token.AccessToken,
                          this.OrderDetailsID);

            if (!response3.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response3.Message,
                    Languages.Accept);
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Home = new MasterDetailPage1DetailViewModel();
            Application.Current.MainPage = new MasterDetailPage1();
            await PopupNavigation.Instance.PopAsync(true);
        }

        #endregion
    }
}
