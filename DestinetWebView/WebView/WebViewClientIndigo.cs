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
    public class WebViewClientIndigo : WebViewClient
    {
        public Activity Context { get; set; }
        public ProgressBar LoadingImage { get; set; }
        public FrameLayout WebViewPlaceHolder { get; set; }
        
        public override void OnPageStarted(Android.Webkit.WebView view, string url, Bitmap favicon)
        {
            base.OnPageStarted(view, url, favicon);
            if (DroidUtility.IsConnectingToInternet(Context))
            {
                WebViewPlaceHolder.Visibility = ViewStates.Gone;
                LoadingImage.Visibility = ViewStates.Visible;
            }
            else
            {
                Intent intent = new Intent(Context,typeof(NoInternetActivity));
                intent.PutExtra("url", url);
                Context.StartActivity(intent);
            }
        }
        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);
            LoadingImage.Visibility = ViewStates.Gone;
            WebViewPlaceHolder.Visibility = ViewStates.Visible;
        }
        //public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, string url)
        //{
        //    url = url.ToLower();
        //    if (url.StartsWith("http://") || url.StartsWith("https://"))
        //    {
        //        string customerDomain = DroidUtility.GetUrl(Context).ToLower();
        //        if (!string.IsNullOrWhiteSpace(customerDomain))
        //        {
        //            customerDomain = customerDomain.Replace("http://", string.Empty).Replace("https://", string.Empty).TrimEnd('/');
        //            if (!url.Contains(customerDomain))
        //            {
        //                Intent intentOther = new Intent(Context, typeof(WebViewActivityOther));
        //                intentOther.PutExtra("url", url);
        //                Context.StartActivity(intentOther);
        //                return true;
        //            }
        //            if (url.ToLower().EndsWith("/hjem"))
        //            {
        //                url = url + "?AndroidDeviceID=" + DroidUtility.GetToken(Context);
        //                Intent intentOther = new Intent(Context, typeof(WebViewActivity));
        //                intentOther.PutExtra("url", url);
        //                Context.StartActivity(intentOther);
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //    // Otherwise allow the OS to handle things like tel, mailto, etc.
        //    Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(url));
        //    Context.StartActivity(intent);
        //    return true;
        //}
        //public override bool ShouldOverrideUrlLoading(Android.Webkit.WebView view, IWebResourceRequest request)
        //{
        //    string url = request.Url.ToString().ToLower();
        //    if (url.StartsWith("http://") || url.StartsWith("https://"))
        //    {
        //        string customerDomain = DroidUtility.GetUrl(Context).ToLower();
        //        if (!string.IsNullOrWhiteSpace(customerDomain))
        //        {
        //            customerDomain = customerDomain.Replace("http://", string.Empty).Replace("https://", string.Empty).TrimEnd('/');
        //            if (!url.Contains(customerDomain))
        //            {
        //                Intent intentOther = new Intent(Context, typeof(WebViewActivityOther));
        //                intentOther.PutExtra("url", url);
        //                Context.StartActivity(intentOther);
        //                return false;
        //            }
        //        }

        //    }
        //    // Otherwise allow the OS to handle things like tel, mailto, etc.
        //    Intent intent = new Intent(Intent.ActionView, request.Url);
        //    Context.StartActivity(intent);
        //    return true;
        //}
    }
}