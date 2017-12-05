namespace CardProject.CardEffects.Instant
{
    public interface IInstant : ICardEffect
    {
        void Trigger(OwnedCard card);
    }
}