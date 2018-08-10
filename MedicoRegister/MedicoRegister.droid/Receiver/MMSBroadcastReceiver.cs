//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Android.Telephony;
//using Android.Util;
//using Android.Database;
//using static Android.Provider.Telephony.Mms;
//using Android.Graphics;
//using Java.IO;

//namespace MedicoRegister.droid.Receiver
//{


//    //[Android.Runtime.Register("parse", "(Ljava/lang/String;)Landroid/net/Uri;", "")]
//    //[Android.Runtime.Register("openInputStream", "(Landroid/net/Uri;)Ljava/io/InputStream;", "")]

//    //[BroadcastReceiver(Enabled = true, Label = "MMS Receiver")]
//    //[BroadcastReceiver]
//    //[IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED", "android.provider.Telephony.WAP_PUSH_RECEIVED" })]

//    [BroadcastReceiver]
//    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
//    public class MMSBroadcastReceiver : BroadcastReceiver
//    {

//        public event EventHandler<String> onSMSReceived;
//        //public event EventHandler<Bitmap> onMMSReceived;

//        private const string Tag = "MMSBroadcastReceiver";
//        private const string SMSIntentAction = "android.provider.Telephony.SMS_RECEIVED";
//        //private const string MMSIntentAction = "android.provider.Telephony.WAP_PUSH_RECEIVED";


//        public override void OnReceive(Context context, Intent intent)
//        {

//            Log.Info(Tag, "Intent received: " + intent.Action); 

//            //if (intent.Action == SMSIntentAction)
//            //    NotifySMS(context, intent);

//            if (intent.Action == MMSIntentAction || intent.Action == SMSIntentAction)
//            {
//                var uri = Android.Net.Uri.Parse("content://mms-sms/conversations/");
//                //string[] projection = new String[] { "_id", "ct_t" };
//                string[] projection = new String[] { "*" };
//                // CursorLoader introduced in Honeycomb (3.0, API11)
//                //var loader = new CursorLoader(context, uri, projection, null, null, null);
//                //var cursor = (ICursor)loader.LoadInBackground();
//                MainActivity activity = context as MainActivity;
//                ICursor cursor  = null;
//                try
//                {
//                    cursor = activity.ContentResolver.Query(uri, projection, null, null, null);

//                }
//                catch (Exception e)
//                {
//                    //Log.Info(Tag, "Cursor exception: " + cursor.GetColumnNames().Count());
//                    int x = 0;
//                }
//                if (cursor.MoveToFirst())
//                {
//                    do
//                    {
//                        String str = cursor.GetString(cursor.GetColumnIndex("ct_t"));
//                        if ("application/vnd.wap.multipart.related".Equals(str))
//                        {
//                            // it's MMS
//                            String partId = cursor.GetString(cursor.GetColumnIndex("_id"));
//                            String type = cursor.GetString(cursor.GetColumnIndex("ct"));

//                            if ("image/jpeg".Equals(type) || "image/bmp".Equals(type) ||
//                                "image/gif".Equals(type) || "image/jpg".Equals(type) ||
//                                "image/png".Equals(type))
//                            {
//                                //Bitmap bitmap = getMmsImage(partId);
//                                Byte[] bitmapArrayOfByte = cursor.GetBlob(4);
//                                Bitmap bitmap = BitmapFactory.DecodeByteArray(bitmapArrayOfByte, 0, bitmapArrayOfByte.Length);

//                                onMMSReceived?.Invoke(this, bitmap);
//                            }
//                        }
//                        else
//                        {
//                            // it's SMS
//                        }

//                    } while (cursor.MoveToNext());

//                    cursor.Close();
//                }

//                // ContentResolver contentResolver = new ContentResolver(context);
//                //String[] projection = new String[] { "*" };
//                // Android.Net.Uri uri;
//                //Uri.TryCreate("content://mms-sms/conversations/", UriKind.RelativeOrAbsolute, out uri);
//                //ICursor query = contentResolver.Query(uri, projection, null, null, null);
//            }

//            else return;
//        }

//        private Bitmap getMmsImage(string partId)
//        {
//            // Android.Net.Uri partURI = Android.Net.Uri.Parse("content://mms/part/" + partId);

//            var contentURI = Android.Net.Uri.Parse("content://mms/part/");

//            var partURI = Android.Net.Uri.WithAppendedPath(contentURI, partId);

//            InputStream inputStream = null;
//            Bitmap bitmap = null;
//            try
//            {
//                //inputStream = ContentResolver.  .openInputStream(partURI);
//                //bitmap = BitmapFactory.DecodeByteArray.DecodeStream(inputStream);
//            }
//            catch (IOException e) { }
//            finally
//            {
//                if (inputStream != null)
//                {
//                    try
//                    {
//                        inputStream.Close();
//                    }
//                    catch (IOException e) { }
//                }
//            }
//            return bitmap;
//        }

//        private void NotifySMS(Context context, Intent intent)
//        {
//            var bundle = intent.Extras;

//            if (bundle == null) return;

//            var pdus = bundle.Get("pdus");
//            var castedPdus = JNIEnv.GetArray<Java.Lang.Object>(pdus.Handle);

//            var msgs = new SmsMessage[castedPdus.Length];

//            var sb = new StringBuilder();

//            for (var i = 0; i < msgs.Length; i++)
//            {
//                var bytes = new byte[JNIEnv.GetArrayLength(castedPdus[i].Handle)];
//                JNIEnv.CopyArray(castedPdus[i].Handle, bytes);

//                msgs[i] = SmsMessage.CreateFromPdu(bytes);

//                sb.Append(string.Format("SMS From: {0}{1}Body: {2}{1}", msgs[i].OriginatingAddress,
//                                       System.Environment.NewLine, msgs[i].MessageBody));
//            }

//            Toast.MakeText(context, sb.ToString(), ToastLength.Long).Show();
//        }
//    }
//}