using System.Collections.Generic;
using System.Linq;

public class DrawCardEffect : IQuantifiableInstantCardEffect, IPlayerCardTypeEnumerableInstantCardEffect
{
    public int NumberOfCards;
    public string CardType;

    public void Trigger(Card card)
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

    public void Trigger(Card card, int quantity)
    {
        NumberOfCards = quantity;
        Trigger(card);
    }

    public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(Card card)
    {
        return card.Owner.DrawCards(NumberOfCards, CardType);
    }

    public int TriggerWithCount(Card card)
    {
        return TriggerWithPlayerCardTypes(card).Count();
    }
}