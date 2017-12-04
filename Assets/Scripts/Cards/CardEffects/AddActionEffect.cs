public class AddActionEffect : IQuantifiableInstantCardEffect
{
    public int ActionDelta;

    public void Trigger(Card card)
    {
        card.Owner.AddAction(ActionDelta);
    }

    public string GetText()
    {
        return string.Format("Action {0}.", ActionDelta.ToStringWithPlus());
    }

    public void Trigger(Card card, int quantity)
    {
        ActionDelta = quantity;
        Trigger(card);
    }
}