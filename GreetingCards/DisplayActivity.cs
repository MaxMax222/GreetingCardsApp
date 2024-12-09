
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
            BirthdayYoung,
            BirthdayAdult,
            All
        }

        Dictionary<int, FormType> variants;
        Button ret;
        CardsRepo repo;
        ListView cardsView;
        Spinner options; 
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
            ret.Click += Ret_Click;
        }

        private void Ret_Click(object sender, EventArgs e)
        {
            Finish();
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
            else if(typeOfCards == FormType.BirthdayAdult)
            {
                var filtered = repo.Filter<AdultBirthCard>();
                foreach (var card in filtered)
                {
                    showCards.Add(card);
                }
            }
            else if(typeOfCards == FormType.BirthdayYoung)
            {
                var filtered = repo.Filter<YouthBirthCard>();
                foreach (var card in filtered)
                {
                    showCards.Add(card);
                }
            }
            else
            {
                showCards = repo.getAll();
            }
            var adapter = new CardsAdapter(this, showCards);
            cardsView.Adapter = adapter;

     
        }

        private void Init()
        {
            variants = new Dictionary<int, FormType> { { 0, FormType.Wedding }, { 1, FormType.BirthdayAdult }, {2, FormType.BirthdayYoung },{3, FormType.All} };

            ret = FindViewById<Button>(Resource.Id.ret);
            cardsView = FindViewById<ListView>(Resource.Id.cardsDisplay);
            options = FindViewById<Spinner>(Resource.Id.options);

            var items = new List<string> { "Wedding cards", "Adult Birthday cards", "Young Birthday cards", "All cards"};

            // Create an ArrayAdapter with the options
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            // Set the adapter to the Spinner
            options.Adapter = adapter;

            // Handle the item selected event
            options.ItemSelected += Options_ItemSelected;
            repo = CardsRepo.GetInstance();
        }

        private void Options_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (variants.ContainsKey(e.Position))
            {
                GenerateForm(variants[e.Position]);
            }
        }
    }
}

