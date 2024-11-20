using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System;
using Android.Content;

namespace GreetingCards
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button wedding, birthday, display;
        Intent createIntent, displayIntent;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Init();
            AddClicks();
        }

        private void AddClicks()
        {
            wedding.Click += Wedding_Click;
            birthday.Click += Birthday_Click;
            display.Click += Display_Click;
        }

        private void Display_Click(object sender, EventArgs e)
        {
            StartActivity(displayIntent);
        }

        private void Birthday_Click(object sender, EventArgs e)
        {
            createIntent.PutExtra("type", 1);
            StartActivity(createIntent);
        }

        private void Wedding_Click(object sender, EventArgs e)
        {
            createIntent.PutExtra("type", 2);
            StartActivity(createIntent);
        }

        private void Init()
        {
            wedding = FindViewById<Button>(Resource.Id.wedding);
            birthday = FindViewById<Button>(Resource.Id.birthday);
            display = FindViewById<Button>(Resource.Id.display);

            createIntent = new Intent(this, typeof(CreateActivity));
            displayIntent = new Intent(this, typeof(DisplayActivity));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
