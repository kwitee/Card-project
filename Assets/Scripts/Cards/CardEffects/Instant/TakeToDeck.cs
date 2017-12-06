namespace CardProject.Cards.CardEffects.Instant
{
    public class TakeToDeck : IInstant
    {
        public string CardName;
        public int NumberOfCards;

        public void Trigger(OwnedCard card)
        {
            card.Owner.Deck.AddNewCard(CardName, NumberOfCards);
        }

        public string GetText()
        {
            if (NumberOfCards == 1)
                return string.Format("Take a <i>{0}</i> to your deck.", CardName);
            else
                return string.Format("Take {0} <i>{1}</i>'s to your deck.", NumberOfCards.ToString(), CardName);
        }
    }
}