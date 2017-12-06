using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instant
{
    public class DrawCard : IQuantifiable, IPlayerCardTypeEnumerable
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

        public void Trigger(OwnedCard card, int quantity)
        {
            NumberOfCards = quantity;
            Trigger(card);
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(OwnedCard card)
        {
            return card.Owner.DrawCards(NumberOfCards, CardType);
        }

        public int TriggerWithCount(OwnedCard card)
        {
            return TriggerWithPlayerCardTypes(card).Count();
        }
    }
}