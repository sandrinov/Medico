using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;

using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Telephony;
using Android.Util;
using Android.Database;
using static Android.Provider.Telephony.Mms;
using Android.Graphics;
using Java.IO;

namespace MedicoRegister.droid.Receiver
{

    [BroadcastReceiver]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
    public class SMSBroadcastReceiver : BroadcastReceiver
    {

        public event EventHandler<String> onSMSReceived;

        private const string Tag = "SMSBroadcastReceiver";
        private const string SMSIntentAction = "android.provider.Telephony.SMS_RECEIVED";
     
        public override void OnReceive(Context context, Intent intent)
        {

            Log.Info(Tag, "Intent received: " + intent.Action);


            if (intent.Action == SMSIntentAction)
            {
                var bundle = intent.Extras;

                if (bundle == null) return;

                var pdus = bundle.Get("pdus");
                var castedPdus = JNIEnv.GetArray<Java.Lang.Object>(pdus.Handle);

                var msgs = new SmsMessage[castedPdus.Length];

                var sb = new StringBuilder();

                for (var i = 0; i < msgs.Length; i++)
                {
                    var bytes = new byte[JNIEnv.GetArrayLength(castedPdus[i].Handle)];
                    JNIEnv.CopyArray(castedPdus[i].Handle, bytes);

                    msgs[i] = SmsMessage.CreateFromPdu(bytes);

                    sb.Append(string.Format("{1}*{0}", msgs[i].OriginatingAddress, msgs[i].MessageBody));
                }

                onSMSReceived?.Invoke(this, sb.ToString());
            }

            else return;
        }//end onReceive
    }//end broadcast receiver
}//end namespace
