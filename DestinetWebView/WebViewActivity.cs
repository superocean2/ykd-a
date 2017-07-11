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
using Android.Webkit;
using Android.Content.Res;
using Android.Provider;
using Android.Net;
using System.Threading;
using Java.Interop;

namespace DestinetWebView
{
    [Activity(Label = "TubeKid.net", MainLauncher = true,Icon = "@mipmap/ic_launcher", Theme = "@style/AppTheme.NoActionBar", ConfigurationChanges = Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class WebViewActivity : Activity
    {
        FrameLayout webViewPlaceHolder;
        WebView webView;
        ProgressBar imageLoading;
        string url = IndigoConfig.WEB_URL;
        WebViewClientIndigo webViewClient;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.webviewLayout);
            if (!string.IsNullOrWhiteSpace(Intent.GetStringExtra("url")))
            {
                url = Intent.GetStringExtra("url");
            }
            webViewPlaceHolder = (FrameLayout)FindViewById(Resource.Id.webViewPlaceholder);
            imageLoading = (ProgressBar)FindViewById(Resource.Id.loading);
            imageLoading.Visibility = ViewStates.Gone;
            webViewClient = new WebViewClientIndigo() { Context = this, LoadingImage = imageLoading, WebViewPlaceHolder = webViewPlaceHolder };
            InitializeUI();
        }
        protected override void OnResume()
        {
            base.OnResume();
        }
        protected override void OnPause()
        {
            base.OnPause();
        }
        public override void OnBackPressed()
        {
            webView.GoBack();
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }
        private void InitializeUI()
        {
            if (webView == null)
            {
                webView = new WebView(this);
                webView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                webView.Settings.SetSupportZoom(true);
                webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
                webView.ScrollbarFadingEnabled = true;
                webView.Settings.JavaScriptEnabled = true;
                webView.SetWebViewClient(webViewClient);
                // Load a page
                webView.LoadUrl(url);
                //add webview to placeholder
                webViewPlaceHolder.AddView(webView);
            }

        }
    }
}