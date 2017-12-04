using System;
using System.Collections.Generic;
using System.Linq;

public class CardsInHandEffect : IPlayerCardTypeEnumerableInstantCardEffect
{
    public string GetText()
    {
        throw new NotSupportedException();
    }

    public void Trigger(Card card)
    {
        throw new NotSupportedException();
    }

    public int TriggerWithCount(Card card)
    {
        return card.Owner.Hand.GetCards().Count();
    }

    public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(Card card)
    {
        return card.Owner.Hand.GetCards().Select(c => c.PlayerCard.Type);
    }
}