public class AddStarvationEffect : IQuantifiableInstantCardEffect
{
    public int StarvationDelta;

    public void Trigger(Card card)
    {
        card.Owner.AddStarvation(StarvationDelta);
    }

    public string GetText()
    {
        return string.Format("Starvation {0}.", StarvationDelta.ToStringWithPlus());
    }

    public void Trigger(Card card, int quantity)
    {
        StarvationDelta = quantity;
        Trigger(card);
    }
}