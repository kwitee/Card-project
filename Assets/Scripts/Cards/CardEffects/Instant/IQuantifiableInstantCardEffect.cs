public interface IQuantifiableInstantCardEffect : IInstantCardEffect
{
    void Trigger(OwnedCard card, int quantity);
}