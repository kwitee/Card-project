namespace CardProject.CardEffects.Instant
{
    public interface IQuantifiable : IInstant
    {
        void Trigger(OwnedCard card, int quantity);
    }
}