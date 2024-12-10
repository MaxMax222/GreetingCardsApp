
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Systems;
using Android.Views;
using Android.Widget;

namespace GreetingCards
{
	[Activity (Label = "DisplayCreatedActivity")]			
	public class DisplayCreatedActivity : Activity
	{
		CardsRepo repo;
		LinearLayout main;
		Button ret;
		Dialog dialog;
		GreetingCard card;
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
			ret = FindViewById<Button>(Resource.Id.ret);
			ret.Click += Ret_Click;

			card = repo.GetLast();
			dialog = new Dialog(this);
			ShowCard();


		}

        private void ShowCard()
        {
            dialog.SetCanceledOnTouchOutside(true);
            dialog.SetContentView(Resource.Layout.showDialog);
            var mainD = dialog.FindViewById<LinearLayout>(Resource.Id.main);
            if (card is WeddingCard)
            {
                mainD.SetBackgroundResource(Resource.Drawable.weddingCard);
            }
            else if (card is AdultBirthCard)
            {
                mainD.SetBackgroundResource(Resource.Drawable.adultBirthday);
            }
            else
            {
                mainD.SetBackgroundResource(Resource.Drawable.youngBirthday);
            }

            var greeting = dialog.FindViewById<TextView>(Resource.Id.greeting);
            greeting.Text = card.GreetingMSG();
            dialog.Show();
        }

        private void Ret_Click(object sender, EventArgs e)
        {
			Finish();
        }
    }
}

