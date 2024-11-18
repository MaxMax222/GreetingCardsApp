
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

namespace GreetingCards
{
	[Activity (Label = "DisplayCreatedActivity")]			
	public class DisplayCreatedActivity : Activity
	{
		CardsRepo repo;
		TextView card;
		LinearLayout main;
		Button ret;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.DisplayCreated);
			Init();
		}

        private void Init()
        {
			repo = CardsRepo.GetInstance();

            main = FindViewById<LinearLayout>(Resource.Id.main);

            card = new TextView(this) { TextSize = 25 };
            card.Text = repo.GetLast().GreetingMSG();
			main.AddView(card);

			ret = new Button(this) { Text = "Return"};
            ret.Click += Ret_Click;
			main.AddView(ret);
		}

        private void Ret_Click(object sender, EventArgs e)
        {
			Finish();
        }
    }
}

