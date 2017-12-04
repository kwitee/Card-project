public interface IQuantifiableInstantCardEffect : IInstantCardEffect
{
    void Trigger(Card card, int quantity);
}