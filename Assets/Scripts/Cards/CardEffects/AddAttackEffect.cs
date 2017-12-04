public class AddAttackEffect : IQuantifiableInstantCardEffect
{
    public int AttackDelta;

    public void Trigger(Card card)
    {
        card.Owner.AddAttack(AttackDelta);
    }

    public string GetText()
    {
        return string.Format("Attack {0}.", AttackDelta.ToStringWithPlus());
    }

    public void Trigger(Card card, int quantity)
    {
        AttackDelta = quantity;
        Trigger(card);
    }
}