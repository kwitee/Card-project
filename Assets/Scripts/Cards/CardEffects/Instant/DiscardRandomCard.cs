using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instant
{
    public class DiscardRandomCard : IPlayerCardTypeEnumerable
    {
        public int NumberOfCards;
        public string CardType;

        public void Trigger(OwnedCard card)
        {
            TriggerWithPlayerCardTypes(card);
        }

        public string GetText()
        {
            if (NumberOfCards == 1)
            {
                if (CardType == null)
                    return string.Format("Discard random card in hand.");
                else
                    return string.Format("Discard random <i>{0}</i> card in hand.", CardType);
            }
            else
            {
                if (CardType == null)
                    return string.Format("Discard {0} random cards in hand.", NumberOfCards);
                else
                    return string.Format("Discard {0} <i>{1}</i> random cards in hand.", NumberOfCards, CardType);
            }
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(OwnedCard card)
        {
            return card.Owner.Hand.DiscardRandomCard(NumberOfCards, CardType);
        }

        public int TriggerWithCount(OwnedCard card)
        {
            return TriggerWithPlayerCardTypes(card).Count();
        }
    }
}