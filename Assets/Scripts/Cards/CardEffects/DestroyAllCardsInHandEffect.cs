using System.Collections.Generic;
using System.Linq;

public class DestroyAllCardsInHandEffect : IPlayerCardTypeEnumerableInstantCardEffect
{
    public string CardType;

    public void Trigger(Card card)
    {
        TriggerWithPlayerCardTypes(card);
    }

    public string GetText()
    {
        if (CardType == null)
            return string.Format("Destroy all cards in hand.");
        else
            return string.Format("Destroy all <i>{0}</i> cards in hand.", CardType);
    }

    public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(Card card)
    {
        return card.Owner.Hand.DestroyAllCards(CardType);
    }

    public int TriggerWithCount(Card card)
    {
        return TriggerWithPlayerCardTypes(card).Count();
    }
}