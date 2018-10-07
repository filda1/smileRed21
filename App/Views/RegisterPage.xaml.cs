﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smileRed21.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage (string infoPlace)
		{
			InitializeComponent ();
            PlaceLabel.Text = infoPlace;
            PlaceLabel.HorizontalTextAlignment= TextAlignment.Start;
        }
	}
}