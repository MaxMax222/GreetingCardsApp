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
        Button create, display;
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
            display.Click += Display_Click;
            create.Click += Create_Click;
        }

        private void Display_Click(object sender, EventArgs e)
        {
            StartActivity(displayIntent);
        }

        private void Create_Click(object sender, EventArgs e)
        {
            StartActivity(createIntent);
        }
        private void Init()
        {
            create = FindViewById<Button>(Resource.Id.create);
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
