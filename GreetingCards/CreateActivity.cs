
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
using Org.Apache.Commons.Logging;

namespace GreetingCards
{
    [Activity(Label = "CreateActivity")]
    public class CreateActivity : Activity
    {
        private EditText cardSenderWedd, groom, bride;
        private EditText cardSender, cardRecipient, age;
        private Button createWedd, createBirth;

        private CardsRepo repo;
        private Dialog dialog;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Create);

            Init();

            // Set onClick listeners for buttons
            createWedd.Click += CreateWeddOnClick;
            createBirth.Click += CreateOnClick;
        }

        [Obsolete]
        private void Init()
        {
            // Setup the TabHost
            var tabHost = FindViewById<TabHost>(Resource.Id.tabHost);
            tabHost.Setup();

            // Initialize views for Wedding Tab
            cardSenderWedd = tabHost.FindViewById<EditText>(Resource.Id.cardSenderWedd);
            groom = tabHost.FindViewById<EditText>(Resource.Id.groom);
            bride = tabHost.FindViewById<EditText>(Resource.Id.bride);
            createWedd = tabHost.FindViewById<Button>(Resource.Id.createWedd);

            // Initialize views for Birthday Tab
            cardSender = tabHost.FindViewById<EditText>(Resource.Id.cardSender);
            cardRecipient = tabHost.FindViewById<EditText>(Resource.Id.cardRecipient);
            age = tabHost.FindViewById<EditText>(Resource.Id.age);
            createBirth = tabHost.FindViewById<Button>(Resource.Id.create);

            

            // Create tabs
            var weddingTab = tabHost.NewTabSpec("weddingTab").SetIndicator("Wedding").SetContent(Resource.Id.wedding_card);
            var birthdayTab = tabHost.NewTabSpec("birthdayTab").SetIndicator("Birthday").SetContent(Resource.Id.birthday_card);

            tabHost.AddTab(weddingTab);
            tabHost.AddTab(birthdayTab);

            repo = CardsRepo.GetInstance();

            dialog = new Dialog(this);

        }

        private void CreateWeddOnClick(object sender, EventArgs e)
        {
            if (ValidateWeddingInputs())
            {
                repo.AddCard(new WeddingCard(groom.Text, bride.Text, cardSenderWedd.Text));
                Toast.MakeText(this, "Wedding card created!", ToastLength.Short).Show();
                ShowCard();
                
            }
        }

        private void CreateOnClick(object sender, EventArgs e)
        {
            if (ValidateBirthdayInputs())
            {
                if (int.Parse(age.Text) > 17)
                {
                    repo.AddCard(new AdultBirthCard(cardRecipient.Text, cardSender.Text, int.Parse(age.Text)));
                }
                else
                {
                    repo.AddCard(new YouthBirthCard(cardRecipient.Text, cardSender.Text, int.Parse(age.Text)));
                }
                Toast.MakeText(this, "Birthday card created!", ToastLength.Short).Show();
                ShowCard();
            }
        }

        // Wedding Input Validation
        private bool ValidateWeddingInputs()
        {
            if (string.IsNullOrWhiteSpace(cardSenderWedd.Text))
            {
                Toast.MakeText(this, "Sender is required!", ToastLength.Short).Show();
                return false;
            }
            if (string.IsNullOrWhiteSpace(groom.Text))
            {
                Toast.MakeText(this, "Groom is required!", ToastLength.Short).Show();
                return false;
            }
            if (string.IsNullOrWhiteSpace(bride.Text))
            {
                Toast.MakeText(this, "Bride is required!", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        // Birthday Input Validation
        private bool ValidateBirthdayInputs()
        {
            if (string.IsNullOrWhiteSpace(cardSender.Text))
            {
                Toast.MakeText(this, "Sender is required!", ToastLength.Short).Show();
                return false;
            }
            if (string.IsNullOrWhiteSpace(cardRecipient.Text))
            {
                Toast.MakeText(this, "Recipient is required!", ToastLength.Short).Show();
                return false;
            }
            if (string.IsNullOrWhiteSpace(age.Text) || !int.TryParse(age.Text, out _))
            {
                Toast.MakeText(this, "Age is required and should be a number!", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        private void ShowCard()
        {
            dialog.SetCanceledOnTouchOutside(true);
            dialog.SetContentView(Resource.Layout.showDialog);
            var mainD = dialog.FindViewById<LinearLayout>(Resource.Id.main);
            var card = repo.GetLast();
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

            // Set a cancel listener to finish the activity when the dialog is canceled
            dialog.SetOnCancelListener(new DialogInterfaceOnCancelListener(() =>
            {
                Finish(); // Ends the activity
            }));
            dialog.Show();
        }
    }

    public class DialogInterfaceOnCancelListener : Java.Lang.Object, IDialogInterfaceOnCancelListener
    {
        private readonly Action _onCancelAction;

        public DialogInterfaceOnCancelListener(Action onCancelAction)
        {
            _onCancelAction = onCancelAction;
        }

        public void OnCancel(IDialogInterface dialog)
        {
            _onCancelAction?.Invoke();
        }
    }
}

