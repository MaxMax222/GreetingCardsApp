using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Java.Lang;
using Android.App;
using Android.Graphics.Drawables;

namespace GreetingCards
{
	public class CardsAdapter : BaseAdapter<GreetingCard>
	{
        private readonly Context _context;
        private readonly List<GreetingCard> _items;
        private Dialog dialog;
		public CardsAdapter(Context context, List<GreetingCard> list)
		{
            _context = context;
            _items = list;
		}

        public override GreetingCard this[int position]
        {
            get { return _items[position]; }
        }
            public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            
            var card = _items[position];
            var view = convertView;
            view ??= LayoutInflater.From(_context).Inflate(Resource.Layout.Item, parent, false);

            var icon = view.FindViewById<ImageView>(Resource.Id.cardImg);
            var sender = view.FindViewById<TextView>(Resource.Id.cardSender);
            var recipient = view.FindViewById<TextView>(Resource.Id.cardRecipient);
            var btnDisplay = view.FindViewById<Button>(Resource.Id.display);
            int imgId;
            if (card is WeddingCard)
            {
                imgId = Resource.Drawable.weddingCard;
                icon.SetImageResource(imgId);
            }
            else if(card is AdultBirthCard)
            {
                imgId = Resource.Drawable.adultBirthday;
                icon.SetImageResource(imgId);
            }
            else
            {
                imgId = Resource.Drawable.youngBirthday;
                icon.SetImageResource(imgId);
            }

            sender.Text = "Sender: " + card.Sender;
            recipient.Text = "Recipient: " + card.Recipient;

            btnDisplay.Tag = position;
            btnDisplay.Click += BtnDisplay_Click;
            return view;
        }

        private void BtnDisplay_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int pos = (int)btn.Tag;
            ShowCard(pos);
        }

        private void ShowCard(int pos)
        {
            dialog = new Dialog(_context);
            dialog.SetCanceledOnTouchOutside(true);
            dialog.SetContentView(Resource.Layout.showDialog);

            var imgCard = dialog.FindViewById<ImageView>(Resource.Id.cardImg);
            var card = _items[pos];
            if (card is WeddingCard)
            {
                imgCard.SetImageResource(Resource.Drawable.weddingCard);
            }
            else if(card is AdultBirthCard)
            {
                imgCard.SetImageResource(Resource.Drawable.adultBirthday);
            }
            else
            {
                imgCard.SetImageResource(Resource.Drawable.youngBirthday);
            }

            var greeting = dialog.FindViewById<TextView>(Resource.Id.greeting);
            greeting.Text = card.GreetingMSG();
            dialog.Show();
        }
    }
}

