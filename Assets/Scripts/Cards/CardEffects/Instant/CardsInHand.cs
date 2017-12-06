using System;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instant
{
    public class CardsInHand : IPlayerCardTypeEnumerable
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
            return card.Owner.Hand.GetCards().Count();
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(OwnedCard card)
        {
            return card.Owner.Hand.GetCards().Select(c => c.PlayerCard.Type);
        }
    }
}