using System;

namespace CardProject.CardEffects.Instant
{
    public class CountCards : ICountable
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
            return card.Owner.GetNumberOfAllCards();
        }
    }
}