using smileRed25.Domain;
using smileRed25.Helpers;
using smileRed25.Models;
using smileRed25.Services;
using smileRed25.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace smileRed25.ViewModels
{
  public  class MasterDetailPage1DetailViewModel:BaseViewModel
  {
      /*  #region Services
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
*/
        #region Constructors
        public MasterDetailPage1DetailViewModel()
        {
            /* this.User = MainViewModel.GetInstance().User;
             this.apiService = new ApiService();
             this.dataService = new DataService*/

            // this.LoadOrders(); // Sale Error
        }
        #endregion

       // #region Methods
      /*  private async void LoadOrders()
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

        }*
        #endregion*/

    }
}
