using System;

namespace CardProject.Cards.CardEffects.Instant
{
    public class CountCardsPlayed : ICountable
    {
        public string GetText()
        {
            throw new NotSupportedException();
        }

        public void Trigger(OwnedCard card)
        {
            throw new NotSupportedException();
        }

        public int TriggerWithCount(OwnedCard card)
        {
            return card.Owner.GetNumberOfPlayedCards();
        }
    }
}