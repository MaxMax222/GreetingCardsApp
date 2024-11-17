using System;
namespace GreetingCards
{
	public class BirthdayCard : GreetingCard
	{
		public int Age { get; }
		public BirthdayCard(string recipient, string sender, int age): base(recipient, sender) {Age = age;}

		public override string GreetingMSG() => $"Have a happy {Age} birthday {Recipient}, from {Sender}";

		public override string ToString() => $"{Recipient} is turning {Age}, Sender: {Sender}";
    }
}

