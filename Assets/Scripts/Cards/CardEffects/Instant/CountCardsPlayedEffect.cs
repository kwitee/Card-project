using System;

public class CountCardsPlayedEffect : ICountableInstantCardEffect
{
    public string GetText()
    {
        throw new NotSupportedException();
    }

    public void Trigger(OwnedCard card)
    {
        throw new NotSupportedException();
    }

    public int TriggerWithCount(OwnedCard card)
    {
        return card.Owner.GetNumberOfPlayedCards();
    }
}