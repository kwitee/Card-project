using System.Collections.Generic;
using System.Linq;

namespace CardProject.CardEffects.Instant
{
    public class DiscardAllCards : IPlayerCardTypeEnumerable
    {
        public string CardType;

        public void Trigger(OwnedCard card)
        {
            TriggerWithPlayerCardTypes(card);
        }

        public string GetText()
        {
            if (CardType == null)
                return string.Format("Discard all cards.");
            else
                return string.Format("Discard all {0} cards.", CardType);
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(OwnedCard card)
        {
            return card.Owner.Hand.DiscardAllCards(CardType);
        }

        public int TriggerWithCount(OwnedCard card)
        {
            return TriggerWithPlayerCardTypes(card).Count();
        }
    }
}