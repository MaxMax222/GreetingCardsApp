using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Java.Lang;

namespace GreetingCards
{
	public class CardsAdapter : BaseAdapter<GreetingCard>
	{
        private readonly Context _context;
        private readonly List<GreetingCard> _items;
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
            var item = _items[position];
            var view = convertView;
            view ??= LayoutInflater.From(_context).Inflate(Resource.Layout.Item, parent, false);

            var icon = view.FindViewById<ImageView>(Resource.Id.cardImg);
            var sender = view.FindViewById<TextView>(Resource.Id.cardSender);
            var recipient = view.FindViewById<TextView>(Resource.Id.cardRecipient);

            if (item is WeddingCard)
            {
                icon.SetImageResource(Resource.Drawable.weddingCard);
            }
            else if(item is AdultBirthCard)
            {
                icon.SetImageResource(Resource.Drawable.adultBirthday);
            }
            else
            {
                icon.SetImageResource(Resource.Drawable.youngBirthday);
            }

            sender.Text = "Sender: " + item.Sender;
            recipient.Text = "Recipient: " + item.Recipient;

            return view;
        }
    }
}

