using System;
namespace GreetingCards
{
	public class WeddingCard : GreetingCard
	{
		public WeddingCard(string groom, string bride, string sender) : base($"{groom} and {bride}", sender){}
		public override string GreetingMSG() => $"{Recipient}, have a graet wedding. from {Sender}";
    }
}

