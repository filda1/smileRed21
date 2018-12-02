using GalaSoft.MvvmLight.Command;
using Rg.Plugins.Popup.Services;
using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed25.ViewModels
{
    public class ProductDescriptionPageViewModel : BaseViewModel
    {
      
            #region Services
            private readonly ApiService apiService;
            private readonly DataService dataService;
            #endregion

            #region Attributes
            private string subTitle;
            private string imageFullPath;
            private string price;
            private int productId;
            private string remarks;
            private string quantity;
            private string ingredientsSuggestion;
            private string ingredients;
            #endregion

            #region Propperties 
            public Product product
            {
                get;
                set;
            }
            public Order Order_
            {
                get;
                set;
            }

            public int ID_Order
            {
                get;
                set;
            }

            public DateTime Time
            {
                get;
                set;
            }

            public OrderDetails OrderDetails
            {
                get;
                set;
            }

            public string Price
            {
                get { return this.price; }
                set { SetValue(ref this.price, value); }
            }

            public int ProductId
            {
                get { return this.productId; }
                set { SetValue(ref this.productId, value); }
            }

            public string Remarks
            {
                get { return this.remarks; }
                set { SetValue(ref this.remarks, value); }
            }

            public string ImageFullPath
            {
                get { return this.imageFullPath; }
                set { SetValue(ref this.imageFullPath, value); }
            }

            public string SubTitle
            {
                get { return this.subTitle; }
                set { SetValue(ref this.subTitle, value); }
            }

            public string IngredientsSuggestion
            {
                get { return this.ingredientsSuggestion; }
                set { SetValue(ref this.ingredientsSuggestion, value); }
            }

            public string Quantity
            {
                get { return this.quantity; }
                set { SetValue(ref this.quantity, value); }
            }

            public string Ingredients
            {
                get { return this.ingredients; }
                set { SetValue(ref this.ingredients, value); }
            }

            public int MyOrderId { get; set; }

            public UserLocal User
            {
                get;
                set;
            }

            public int QuantityINT
            {
                get;
                set;
            }

            public ProductsOrders ProductsOrders
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
            public ProductDescriptionPageViewModel(Product product)
            {
                this.User = MainViewModel.GetInstance().User;
                //his.LoadOrders();
                this.apiService = new ApiService();
                this.dataService = new DataService();
                this.product = product;
                this.SubTitle = product.Name;
                this.productId = product.ProductId;
               
                var _price = product.Price;
                CultureInfo portugal = new CultureInfo("pt-PT");
                var numberString = _price.ToString("C2", portugal.NumberFormat);
                this.Price = numberString;

                var remarks = product.Remarks;
                string susti = remarks.Replace("\n", "");
                this.Remarks = susti;

                this.ImageFullPath = product.Image;
                this.Quantity = "1";
            }
            #endregion

            #region Methods

            #endregion


            #region Commands
            public ICommand VerificationLoginCommand
            {
                get
                {
                    return new RelayCommand(VerificationLogin);
                }
            }

            void VerificationLogin()
            {
                if (Settings.IsLogin == "false")
                {
                    Application.Current.MainPage = new NavigationPage(
                    new LoginPage());
                }
                else
                {

                }
            }

            public ICommand VerificationLoginCommand2
            {
                get
                {
                    return new RelayCommand(VerificationLogin2);
                }
            }

            void VerificationLogin2()
            {
                if (Settings.IsLogin == "false")
                {
                    Application.Current.MainPage = new NavigationPage(
                    new LoginPage());
                }
            }

            public ICommand CartCommand
            {
                get
                {
                    return new RelayCommand(Cart);
                }
            }

            private async void Cart()
            {
                if (Settings.IsLogin == "false")
                {

                    Application.Current.MainPage = new NavigationPage(
                    new LoginPage());
                }
                else
                {

                    var apiSecurity = Application.Current.Resources["APISecurity"].ToString();

                    //  Insert OrderId (Compra)
                    var isNumeric = int.TryParse(this.Quantity, out int n);

                    if (!isNumeric)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                         Languages.Error,
                         Languages.QuantityValidation,
                         Languages.Accept);
                        return;
                    }

                    // IsCart= " "         Si es primer registro de Order, IsCart= " "
                    if (string.IsNullOrEmpty(Settings.IsCart))
                    {
                        // Hora Invierno, Cambia (27/28 de Octubre)
                        DateTime thisTime = DateTime.Now;
                        TimeZoneInfo InfoZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                        DateTime TimePT = TimeZoneInfo.ConvertTime(thisTime, TimeZoneInfo.Local, InfoZone);

                        // Horario Verano Cambia (31... de Marzo)
                        // "Romance Standard Time">(GMT+01:00) , Spain, Paris.....

                        // COMPRA (Cart)
                        var order = new Order
                        {
                            OrderStatusID = 2,
                            UserId = this.User.UserId,
                            Email = this.User.Email,
                            DateOrder = TimePT,
                            Delete = false,
                            VisibleOrders = false,
                            ActiveOrders = false,
                        };

                        var response = await this.apiService.Post(
                              apiSecurity,
                              "/api",
                              "/Orders1",
                              MainViewModel.GetInstance().Token.TokenType,
                              MainViewModel.GetInstance().Token.AccessToken,
                              order);

                        int numeric = Convert.ToInt32(this.Quantity);
                        this.QuantityINT = numeric;
                        var orderDetails = new OrderDetails
                        {
                            ProductID = this.productId,
                            Quantity = numeric,
                            VisibleOrderDetails = true,
                            ActiveOrderDetails = true,
                            Ingredients = this.ingredientsSuggestion,
                        };

                        var mainViewModel = MainViewModel.GetInstance();
                        mainViewModel.OptionsOrder = new OptionsOrderViewModel(orderDetails);
                        await PopupNavigation.Instance.PushAsync(new SelectOrderPage());
                    }

                    else
                    {
                        int numeric = Convert.ToInt32(this.Quantity);
                        var orderDetails = new OrderDetails
                        {
                            ProductID = this.productId,
                            Quantity = numeric,
                            VisibleOrderDetails = true,
                            ActiveOrderDetails = true,
                            Ingredients = this.ingredientsSuggestion,
                        };

                       var mainViewModel = MainViewModel.GetInstance();
                        mainViewModel.OptionsOrder = new OptionsOrderViewModel(orderDetails);
                        await PopupNavigation.Instance.PushAsync(new SelectOrderPage());
                    }

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
                }
            }

            public ICommand OrderCommand
            {
                get
                {
                    return new RelayCommand(Order);
                }
            }

            private async void Order()
            {
            if (Settings.IsLogin == "false")
            {
                Application.Current.MainPage = new NavigationPage(
                new LoginPage());
            }
            else

            {
                if (string.IsNullOrEmpty(this.User.Address))
                {
                    await Application.Current.MainPage.DisplayAlert(
                       Languages.Error,
                       "Address empty",
                       Languages.Accept);
                    Application.Current.MainPage = new ProfilePage();

                };

                // Hora Invierno, Cambia (27/28 de Octubre)
                DateTime thisTime = DateTime.Now;
                TimeZoneInfo InfoZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
                DateTime TimePT = TimeZoneInfo.ConvertTime(thisTime, TimeZoneInfo.Local, InfoZone);

                this.Time = TimePT;
                // Horario Verano Cambia (31... de Marzo)
                // Romance Standard Time">(GMT+01:00) , Spain, Paris.....

                 var po = new ProductsOrders
                 {
                     OrderID = this.ID_Order,
                     ProductId = this.productId,
                     Quantity = this.QuantityINT,
                      DateOrder = this.Time,
                    // VisibleOrderDetails = true,
                    // ActiveOrderDetails = true,
                     Ingredients = this.ingredientsSuggestion,
                 };

                Settings.ActiveAddress = "Full";

                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.ModifyAddress = new ModifyAddressViewModel(po);
                await PopupNavigation.Instance.PushAsync(new ModifyAddressPage());
            }        
        }
    }
    #endregion
}

