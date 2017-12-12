using CardProject.Cards.CardTypes.PlayerCardTypes;
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

        public void Trigger(TriggerArgs args)
        {
            throw new NotSupportedException();
        }

        public int TriggerWithCount(TriggerArgs args)
        {
            return args.Player.Hand.GetCards().Count();
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(TriggerArgs args)
        {
            return args.Player.Hand.GetCards().Select(c => c.PlayerCard.Type);
        }
    }
}