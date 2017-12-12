﻿using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instant
{
    public class DestroyAllCardsInHand : IPlayerCardTypeEnumerable
    {
        public string CardType;

        public void Trigger(TriggerArgs args)
        {
            TriggerWithPlayerCardTypes(args);
        }

        public string GetText()
        {
            if (CardType == null)
                return string.Format("Destroy all cards in hand.");
            else
                return string.Format("Destroy all <i>{0}</i> cards in hand.", CardType);
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(TriggerArgs args)
        {
            return args.Player.Hand.DestroyAllCards(CardType);
        }

        public int TriggerWithCount(TriggerArgs args)
        {
            return TriggerWithPlayerCardTypes(args).Count();
        }
    }
}