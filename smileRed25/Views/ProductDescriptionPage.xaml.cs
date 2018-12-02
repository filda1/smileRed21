﻿using Rg.Plugins.Popup.Pages;
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
	public partial class ProductDescriptionPage :  ContentPage
    {
        public string NameTitle
        {
            get;
            set;
        }
        public ProductDescriptionPage (string labelProduct)
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

            this.NameTitle = labelProduct;      
            BindingContext = this.NameTitle;

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