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
using Android.Graphics;

namespace DestinetWebView
{
    public class WebViewClientIndigoOther : WebViewClient
    {
        public Activity Context { get; set; }
        public ProgressBar LoadingImage { get; set; }
        public FrameLayout WebViewPlaceHolder { get; set; }
        public override void OnPageStarted(Android.Webkit.WebView view, string url, Bitmap favicon)
        {
            base.OnPageStarted(view, url, favicon);
            WebViewPlaceHolder.Visibility = ViewStates.Gone;
            LoadingImage.Visibility = ViewStates.Visible;
            TextView title = Context.FindViewById(Resource.Id.title) as TextView;
            if (title != null)
                title.Visibility = ViewStates.Invisible;
        }
        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);
            LoadingImage.Visibility = ViewStates.Gone;
            WebViewPlaceHolder.Visibility = ViewStates.Visible;
            TextView title = Context.FindViewById(Resource.Id.title) as TextView;
            if (title != null)
            {
                title.Text = view.Title;
                title.Visibility = ViewStates.Visible;
            }
                
        }
    }
}