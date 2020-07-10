using Android.App;
using Android.Runtime;
using Android.Util;
using ScanbotSDK.Xamarin.Android;
using ScanbotSDK.Xamarin.Forms;
using System;
using System.IO;
using SBSDKConfiguration = ScanbotSDK.Xamarin.Forms.SBSDKConfiguration;

namespace kyk_ocr.Droid
{
    public class MainApplication : Application
    {
        static string LOG_TAG = typeof(MainApplication).Name;
        const string LICENSE_KEY = null;

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
               : base(javaReference, transfer) { }

        public override void OnCreate()
        {
            base.OnCreate();

            Log.Debug(LOG_TAG, "Initializing Scanbot SDK...");
            var configuration = new SBSDKConfiguration
            {
                EnableLogging = true,
                StorageBaseDirectory = GetDemoStorageBaseDirectory(),
                DetectorType = DocumentDetectorType.MLBased
            };
            SBSDKInitializer.Initialize(this, LICENSE_KEY, configuration);
        }

        string GetDemoStorageBaseDirectory()
        {
            var directory = GetExternalFilesDir(null).AbsolutePath;
            var externalPublicPath = Path.Combine(directory, "my-custom-storage");
            Directory.CreateDirectory(externalPublicPath);
            return externalPublicPath;
        }
    }
}