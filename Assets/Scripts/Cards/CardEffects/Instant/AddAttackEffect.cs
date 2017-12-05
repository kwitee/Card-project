public class AddAttackEffect : IQuantifiableInstantCardEffect
{
    public int AttackDelta;

    public void Trigger(OwnedCard card)
    {
        card.Owner.AddAttack(AttackDelta);
    }

    public string GetText()
    {
        return string.Format("Attack {0}.", AttackDelta.ToStringWithPlus());
    }

    public void Trigger(OwnedCard card, int quantity)
    {
        AttackDelta = quantity;
        Trigger(card);
    }
}