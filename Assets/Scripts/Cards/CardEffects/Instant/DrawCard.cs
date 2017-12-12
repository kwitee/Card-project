﻿using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instant
{
    public class DrawCard : IQuantifiable, IPlayerCardTypeEnumerable
    {
        public int NumberOfCards;
        public string CardType;

        public void Trigger(TriggerArgs args)
        {
            TriggerWithPlayerCardTypes(args);
        }

        public string GetText()
        {
            if (NumberOfCards == 1)
            {
                if (CardType == null)
                    return string.Format("Draw a card.");
                else
                    return string.Format("Draw a <i>{0}</i> card.", CardType);
            }
            else
            {
                if (CardType == null)
                    return string.Format("Draw {0} cards.", NumberOfCards);
                else
                    return string.Format("Draw {0} <i>{1}</i> cards.", NumberOfCards, CardType);
            }
        }

        public void Trigger(TriggerArgs args, int quantity)
        {
            NumberOfCards = quantity;
            Trigger(args);
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(TriggerArgs args)
        {
            return args.Player.DrawCards(NumberOfCards, CardType);
        }

        public int TriggerWithCount(TriggerArgs args)
        {
            return TriggerWithPlayerCardTypes(args).Count();
        }
    }
}