using System;
namespace GreetingCards
{
	public class GreetingCard
	{
		public string Recipient { get; protected set; }
		public string Sender { get; protected set; }
		public GreetingCard(string recipient, string sender)
		{
			Recipient = recipient;
			Sender = sender;
		}

		virtual public string GreetingMSG() => $"{Sender} greets {Recipient}";
		public override string ToString() => $"Sender: {Sender}, Recipient: {Recipient}";

    }
}
