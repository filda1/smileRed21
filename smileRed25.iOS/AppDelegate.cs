using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using UIKit;

namespace smileRed25.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Register Syncfusion license
            //Syncfusion.Licensing.SyncfusionLicenseProvider
            //.RegisterLicense("NTc3MUAzMTM2MmUzMjJlMzBhZUZkRy9EZHlwK1BCTDBsU3FjdW5CR1ZSR21NSlFFMHQ1YWprSDltU0NRPQ==");

            Rg.Plugins.Popup.Popup.Init();

            global::Xamarin.Forms.Forms.Init();

            //CachedImageRenderer.Init();
            ImageCircleRenderer.Init();
            Xamarin.FormsMaps.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
