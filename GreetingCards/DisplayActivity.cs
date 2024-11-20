
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
	[Activity (Label = "DisplayActivity")]			
	public class DisplayActivity : Activity
	{
        private enum FormType
        {
            Wedding,
            Birthday
        }

		Button ret, wedding, birthday;
        LinearLayout main;
        CardsRepo repo;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.Display);
			Init();
			AddClicks();
		}

        private void AddClicks()
        {
            wedding.Click += Wedding_Click;
            birthday.Click += Birthday_Click;
            ret.Click += Ret_Click;
        }

        private void Ret_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Birthday_Click(object sender, EventArgs e)
        {
            main.RemoveAllViews();
            GenerateForm(FormType.Birthday);
        }

        private void Wedding_Click(object sender, EventArgs e)
        {
            main.RemoveAllViews();
            GenerateForm(FormType.Wedding);
        }

        private void GenerateForm(FormType typeOfCards)
        {
            List<GreetingCard> showCards = new List<GreetingCard>();
            if (typeOfCards == FormType.Wedding)
            {
                var filtered = repo.Filter<WeddingCard>();
                foreach (var card in filtered)
                {
                    showCards.Add(card);
                }
            }
            else
            {
                var filtered = repo.Filter<BirthdayCard>();
                foreach (var card in filtered)
                {
                    showCards.Add(card);
                }

            }
            ScrollView mainScroll = new ScrollView(this);
            mainScroll.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, 1);
            LinearLayout linearShow = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
            };
            foreach (var card in showCards)
            {
                TextView msg = new TextView(this);
                msg.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                msg.Text = card.GreetingMSG();
                linearShow.AddView(msg);

            }
            mainScroll.AddView(linearShow);
            main.AddView(mainScroll);
            Button btn = new Button(this);
            {
                LinearLayout.LayoutParams LayoutParameters = new LinearLayout.LayoutParams(-2, -2, 1);
            }
            btn.Text = "close";
            btn.Click += Btn_Click;
            main.AddView(btn);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Init()
        {
			wedding = FindViewById<Button>(Resource.Id.weddings);
			birthday = FindViewById<Button>(Resource.Id.birthdays);
			ret = FindViewById<Button>(Resource.Id.ret);

            main = FindViewById<LinearLayout>(Resource.Id.main);

            repo = CardsRepo.GetInstance();
        }
    }
}

