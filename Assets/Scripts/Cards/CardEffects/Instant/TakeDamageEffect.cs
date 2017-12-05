public class TakeDamageEffect : IQuantifiableInstantCardEffect
{
    public int Damage;

    public void Trigger(OwnedCard card)
    {
        card.Owner.TakeDamage(Damage);
    }

    public string GetText()
    {
        if (Damage >= 0)
            return string.Format("Take {0} damage.", Damage);
        else
            return string.Format("Heal {0}.", -Damage);
    }

    public void Trigger(OwnedCard card, int quantity)
    {
        Damage = quantity;
        Trigger(card);
    }
}