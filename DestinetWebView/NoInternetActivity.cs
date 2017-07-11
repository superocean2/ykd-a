using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DestinetWebView
{
    [Activity(Label = "TubeKid.net", MainLauncher = false, Theme = "@style/AppTheme.NoActionBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class NoInternetActivity : Activity
    {
        string url = IndigoConfig.WEB_URL;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.noInternetLayout);
            if (!string.IsNullOrWhiteSpace(Intent.GetStringExtra("url")))
            {
                url = Intent.GetStringExtra("url");
            }
            ((Button)FindViewById(Resource.Id.tryagain)).Click += (object o, EventArgs e) => {
                Intent intent = new Intent(this, typeof(WebViewActivity));
                intent.PutExtra("url", url);
                StartActivity(intent);
            };
        }
    }
}