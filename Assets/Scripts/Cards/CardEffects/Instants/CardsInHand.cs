using CardProject.Cards.CardTypes.PlayerCardTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instants
{
    public class CardsInHand : IPlayerCardTypeEnumerable
    {
        public string GetText()
        {
            throw new NotSupportedException();
        }

        public void Trigger(InstantTriggerArgs args)
        {
            throw new NotSupportedException();
        }

        public int TriggerWithCount(InstantTriggerArgs args)
        {
            return args.Player.Hand.GetCards().Count();
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(InstantTriggerArgs args)
        {
            return args.Player.Hand.GetCards().Select(c => c.PlayerCard.Type);
        }
    }
}