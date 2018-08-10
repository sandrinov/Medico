using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MedicoRegister.droid.Receiver;
using Android.Graphics;
using Java.Lang;
using MedicoRegister.portable.Models;
using System.Collections.Generic;
using MedicoRegister.portable.Data;
using MedicoRegister.droid.Adapters;
using MedicoRegister.portable.Services;
using MedicoRegister.portable.Helpers;

namespace MedicoRegister.droid
{
    [Activity(Label = "MedicoRegister.droid",
        MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {


        private MedicoUserProvider dataProvider;
        private MedicoServiceClient restClient;
        private SMSBroadcastReceiver smsReceiver;


        protected override void OnStart()
        {
            base.OnStart();
            smsReceiver = new SMSBroadcastReceiver();
            // Register the broadcast receiver
            RegisterReceiver(smsReceiver, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
            smsReceiver.onSMSReceived += SmsReceiver_onSMSReceived;

            //get data provider
            dataProvider = MedicoUserProvider.GetProvider();

            //get service client
            restClient = MedicoServiceClient.GetClient();


        }

        async private void SmsReceiver_onSMSReceived(object sender, string sms)
        {

            string[] smsParts = sms.Split(new char[] { '*' });

            string medicoCode = smsParts[0];
            string phoneNumber = smsParts[1];

            dataProvider.AddMedicoUser(medicoCode, phoneNumber, Resource.Drawable.Icon);
            ListView listView = FindViewById<ListView>(Resource.Id.List);
            listView.Adapter = new MedicoUserListAdapter(this, dataProvider.Items);

            //get an activationcode
            string activationCode = ActivationCodeManager.GetActivationCode();
            
            // timestamp
            string registrationTime = DateTime.Now.ToShortTimeString();

            //get if can register
            RegistrationData data = new RegistrationData()
            {
                ActivationCode = activationCode,
                MedicoCode = medicoCode,
                PhoneNumber = phoneNumber,
                RegistrationTime = registrationTime
            };

            string canRegister = await restClient.CanRegister(data);
            if (canRegister.Equals("ok"))
            {
                var smsUri = Android.Net.Uri.Parse("smsto:" + phoneNumber);
                var smsIntent = new Intent(Intent.ActionSendto, smsUri);
                smsIntent.PutExtra("Codice di attivazione", activationCode);
                StartActivity(smsIntent);
            }

        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterReceiver(smsReceiver);
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(smsReceiver, new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
        }

        protected override void OnStop()
        {
            base.OnStop();
            UnregisterReceiver(smsReceiver);
            // Set the variable to nil, so that we know the receiver is no longer used.
            smsReceiver.onSMSReceived -= SmsReceiver_onSMSReceived;
            smsReceiver = null;
        }
       

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.MyButton);
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}

