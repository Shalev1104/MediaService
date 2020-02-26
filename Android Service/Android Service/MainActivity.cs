using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System;

namespace Android_Service
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button start;
        private Button pause;
        private Button stop;
        private Button secondActivity;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SetViews();
        }
        public void SetViews()
        {
            start = FindViewById<Button>(Resource.Id.btnStart);
            pause = FindViewById<Button>(Resource.Id.btnPause);
            stop = FindViewById<Button>(Resource.Id.btnStop);
            secondActivity = FindViewById<Button>(Resource.Id.btnSecondActivity);

            start.Click += Start_Click;
            pause.Click += Pause_Click;
            stop.Click += Stop_Click;
            secondActivity.Click += SecondActivity_Click;
        }

        private void SecondActivity_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SecondActivity));
            StartActivity(intent);
        }

        private void Stop_Click(object sender, System.EventArgs e)
        {
            Intent intent = GenerateIntent();
            StopService(intent);
        }

        private void Pause_Click(object sender, System.EventArgs e)
        {
            Intent intent = GenerateIntent();
            Intent.PutExtra("ACTION", "Pause");
            StartService(intent);
        }

        public Intent GenerateIntent()
        {
            return new Intent(this, typeof(MediaService));
        }

        private void Start_Click(object sender, System.EventArgs e)
        {
            Intent intent = GenerateIntent();
            StartService(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}