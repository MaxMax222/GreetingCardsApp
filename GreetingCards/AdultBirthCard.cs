using System;
namespace GreetingCards
{
	public class AdultBirthCard : BirthdayCard
	{
        public AdultBirthCard(string recipient, string sender, int age) : base(recipient, sender, age) { }
        public override string GreetingMSG() => $"{Recipient}, Happy {Age} birthday you old little rat, from {Sender}";

    }
}

