using System;
namespace GreetingCards
{
	public class YouthBirthCard : BirthdayCard
	{
		public YouthBirthCard(string recipient, string sender, int age): base(recipient, sender, age){}
		public override string GreetingMSG() => $"{Recipient}, Happy {Age} birthday you little rat, from {Sender}";
    }
}

