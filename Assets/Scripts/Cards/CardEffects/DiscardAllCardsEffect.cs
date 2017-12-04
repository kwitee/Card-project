using System.Collections.Generic;
using System.Linq;

public class DiscardAllCardsEffect : IPlayerCardTypeEnumerableInstantCardEffect
{
    public string CardType;

    public void Trigger(Card card)
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

    public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(Card card)
    {
        return card.Owner.Hand.DiscardAllCards(CardType);
    }

    public int TriggerWithCount(Card card)
    {
        return TriggerWithPlayerCardTypes(card).Count();
    }
}