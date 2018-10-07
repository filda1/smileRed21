using smileRed21.Domain;
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
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public MasterDetailPage1()
        {
            InitializeComponent();

           // App.Navigator = Navigator;
          //  App.Master = this;
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }
      
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Cart = new CartViewModel();
          

            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}