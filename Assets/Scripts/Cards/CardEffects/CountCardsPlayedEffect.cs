using System;

public class CountCardsPlayedEffect : ICountableInstantCardEffect
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
        return card.Owner.GetNumberOfPlayedCards();
    }
}