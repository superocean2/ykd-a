using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Net;
using Android.Widget;
using Android.App;
using Android.Provider;
using Android.Webkit;
using Android.Views;
using Android.Graphics;
using Android.Content.Res;
using System.Net;
using System.Collections.Specialized;
using static Android.Provider.Settings;

namespace DestinetWebView
{
    public class DroidUtility
    {
        //public static bool IsPlayServicesAvailable(Context context)
        //{
        //    int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);
        //    if (resultCode != ConnectionResult.Success)
        //    {
        //        Toast.MakeText(context, "Sorry, this device is not supported", ToastLength.Short);
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        public static bool IsConnectingToInternet(Context context)
        {
            ConnectivityManager connectivity = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            if (connectivity != null)
            {
                NetworkInfo[] info = connectivity.GetAllNetworkInfo();
                if (info != null)
                    for (int i = 0; i < info.Length; i++)
                        if (info[i].GetState() == NetworkInfo.State.Connected)
                        {
                            return true;
                        }

            }
            return false;
        }
        public static void SaveToken(string token, Context context)
        {
            var sharePref = context.GetSharedPreferences("IndigoData", FileCreationMode.Private);
            sharePref.Edit().PutString("Token", token).Commit();
        }
        public static string GetToken(Context context)
        {
            return context.GetSharedPreferences("IndigoData", FileCreationMode.Private).GetString("Token", "");
        }
        public static void SaveUrl(string url, Context context)
        {
            var sharePref = context.GetSharedPreferences("IndigoData", FileCreationMode.Private);
            sharePref.Edit().PutString("Url", url).Commit();
        }
        public static string GetUrl(Context context)
        {
            return context.GetSharedPreferences("IndigoData", FileCreationMode.Private).GetString("Url", "");
        }

        public static void SaveUserData(string data, Context context)
        {
            var sharePref = context.GetSharedPreferences("IndigoData", FileCreationMode.Private);
            sharePref.Edit().PutString("UserData", data).Commit();
        }
        public static string GetUserData(Context context)
        {
            return context.GetSharedPreferences("IndigoData", FileCreationMode.Private).GetString("UserData", "");
        }

        public static void ShowDialogRequireInternet(Activity context, AlertDialog alert)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.SetMessage("This app require internet access. Please enable mobile network or Wi-Fi to use")
                    .SetCancelable(false)
                    .SetPositiveButton("Connect", (dialog, id) =>
                    {
                        context.StartActivity(new Intent(Settings.ActionWifiSettings));
                    })

                   .SetNegativeButton("Quit", (dialog, id) =>
                   {
                       context.Finish();
                   });
            alert = builder.Create();
            alert.Show();
        }

        private static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > reqHeight || width > reqWidth)
            {

                int halfHeight = height / 2;
                int halfWidth = width / 2;

                // Calculate the largest inSampleSize value that is a power of 2 and keeps both
                // height and width larger than the requested height and width.
                while ((halfHeight / inSampleSize) >= reqHeight
                        && (halfWidth / inSampleSize) >= reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return inSampleSize;
        }
        public static Bitmap DecodeBitmapFromResource(Resources res, int resId,int reqWidth, int reqHeight)
        {

            // First decode with inJustDecodeBounds=true to check dimensions
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeResource(res, resId, options);

            // Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);

            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;
            return BitmapFactory.DecodeResource(res, resId, options);
        }
        public static int GetScreenWidth()
        {
            return Resources.System.DisplayMetrics.WidthPixels;
        }

        public static int GetScreenHeight()
        {
            return Resources.System.DisplayMetrics.HeightPixels;
        }

        public static async void PostDeviceTokenIDToServer(string url, string id)
        {
            WebClient webclient = new WebClient();
            var values = new NameValueCollection();
            values.Add("__DOEVENT", "Destinet.Buisness.PushNotification.PushNotification");
            values.Add("__DOEVENTMETHOD", "SaveIndigoRegisId");
            values.Add("__DOEVENTP0", id);
            await webclient.UploadValuesTaskAsync(url, values);
        }
        public static string GetDeviceId(Context context)
        {
            return Secure.GetString(context.ContentResolver,
                                                    Secure.AndroidId);
        }
        public static void NavigateUrl(Context context, string domain, string token, string deviceId,string tokenUsed="")
        {
            string url = domain + $"/api/Indigo/Login/{token}/{deviceId}/Android{tokenUsed}";
            Intent intent = new Intent(context, typeof(WebViewActivity));
            intent.PutExtra("url", url);
            context.StartActivity(intent);
        }

        //public static void Debug(Context context,string message)
        //{
        //    Intent intent = new Intent(context, typeof(ErrorLogActivity));
        //    intent.PutExtra("error", message);
        //    context.StartActivity(intent);
        //}
    }

}