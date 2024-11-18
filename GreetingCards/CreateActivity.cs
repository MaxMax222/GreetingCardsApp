
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
	[Activity (Label = "CreateActivity")]			
	public class CreateActivity : Activity
	{
		EditText editSender, editRecipient, editAge, editGroom, editBride;
		LinearLayout main;
		Button submit;
		CardsRepo repo;
		int formType;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your application here
			SetContentView(Resource.Layout.Create);
			Init();
		}
		
        private void Init()
        {
			editSender = FindViewById<EditText>(Resource.Id.sender);
			main = FindViewById<LinearLayout>(Resource.Id.main);

			repo = CardsRepo.GetInstance();

            formType = Intent.GetIntExtra("type", 0);
            var size_of_edit = new LinearLayout.LayoutParams(-1, -2);
            switch (formType)
			{
				case 1:
					editRecipient = new EditText(this);
					editRecipient.LayoutParameters = size_of_edit;
					editRecipient.Hint = "Recipient:";
					main.AddView(editRecipient);

                    editAge = new EditText(this);
                    editAge.LayoutParameters = size_of_edit;
                    editAge.Hint = "Age:";
                    main.AddView(editAge);
                    break;
				case 2:
                    editGroom = new EditText(this);
                    editGroom.LayoutParameters = size_of_edit;
                    editGroom.Hint = "Groom:";
                    main.AddView(editGroom);

					editBride = new EditText(this);
                    editBride.LayoutParameters = size_of_edit;
                    editBride.Hint = "Bride:";
                    main.AddView(editBride);
					break;
                default:
					break;
			}
            submit = new Button(this);
            submit.LayoutParameters = new LinearLayout.LayoutParams(-2, -2);
            submit.Text = "Submit";
            submit.TextSize = 20;
            submit.Click += Submit_Click;
            main.AddView(submit);
        }

        private void Submit_Click(object sender, EventArgs e)
        {
			var msg = Toast.MakeText(this, "", ToastLength.Long);
			if (ValidateForm())
			{
				if (formType == 1)
				{
					int age = int.Parse(editAge.Text);
					if (age > 17)
					{
						repo.AddCard(new AdultBirthCard(editRecipient.Text, editSender.Text, age));
						msg.SetText("Adult birthday card created");
					}
					else
					{
                        repo.AddCard(new YouthBirthCard(editRecipient.Text, editSender.Text, age));
                        msg.SetText("Youth birthday card created");
                    }
                }
				else
				{
					repo.AddCard(new WeddingCard(editGroom.Text, editBride.Text, editSender.Text));
                    msg.SetText("Wedding card created");
                }
            }
			else
			{
                msg.SetText("Invalid form, try again");
            }
			msg.Show();
			var intent = new Intent(this, typeof(DisplayCreatedActivity));
			StartActivity(intent);
			Finish();
        }

		private bool ValidateForm()
		{
			bool result = editSender.Text != "";
			if (formType == 1)
			{
				result = editRecipient.Text != "" && int.TryParse(editAge.Text, out _);
			}
			else{
				result = editGroom.Text != "" && editBride.Text != "";
			}

			return result;
		}
    }
}

