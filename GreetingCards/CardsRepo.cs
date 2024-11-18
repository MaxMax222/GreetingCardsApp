using System.Collections.Generic;

namespace GreetingCards
{
    public class CardsRepo
    {
        //Private static field to hold the single instance
        private static CardsRepo _instance;
        //Static list to hold greeting cards
        private List<GreetingCard> greetingCards;

        //Private constructor to prevent direct instantiation
        private CardsRepo()
        {
            greetingCards = new List<GreetingCard>();
        }

        //Public method to provide global access to the instance
        public static CardsRepo GetInstance()
        {
            // Lazy initialization: create the instance only when needed
            if (_instance == null)
            {
                _instance = new CardsRepo();
            }
            return _instance;
        }

        // Method to add a card
        public void AddCard(GreetingCard card)
        {
            greetingCards.Add(card);
        }

        public GreetingCard GetLast()
        {
            if (greetingCards.Count > 0)
            {
                return greetingCards[greetingCards.Count - 1];
            }
            else
            {
                return null;
            }
        }
        // Method to filter cards by type
        public List<T> Filter<T>()
        {
            List<T> filtered = new List<T>();
            foreach (GreetingCard card in greetingCards)
            {
                if (card is T filteredCard)
                {
                    filtered.Add(filteredCard);
                }
            }
            return filtered;
        }
    }
}
