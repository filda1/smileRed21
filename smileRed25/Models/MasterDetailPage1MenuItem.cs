//using GalaSoft.MvvmLight.Command;
using smileRed25.Helpers;
//using smileRed25.Services;
using smileRed25.ViewModels;
using smileRed25.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace smileRed25.Models
{

    public class MasterDetailPage1MenuItem
    {

        #region Properties
        public int Id { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
        #endregion

        public MasterDetailPage1MenuItem()
        {
            TargetType = typeof(MasterDetailPage1Detail);
        }

      /*  #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        private void Navigate()
        {
          //  App.Master.IsPresented = false;
   
                Settings.IsRemembered = "false";
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = null;
                mainViewModel.User = null;
                Application.Current.MainPage = new NavigationPage(
                    new LoginPage());                   
        }
        #endregion */


    }
}