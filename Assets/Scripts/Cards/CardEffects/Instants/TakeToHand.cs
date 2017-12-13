namespace CardProject.Cards.CardEffects.Instants
{
    public class TakeToHand : IInstant
    {
        public string CardName;
        public int NumberOfCards;

        public void Trigger(InstantTriggerArgs args)
        {
            args.Player.Hand.AddNewCard(CardName, NumberOfCards);
        }

        public string GetText()
        {
            if (NumberOfCards == 1)
                return string.Format("Take a <i>{0}</i> to your hand.", CardName);
            else
                return string.Format("Take {0} <i>{1}</i>'s to your hand.", NumberOfCards.ToString(), CardName);
        }
    }
}