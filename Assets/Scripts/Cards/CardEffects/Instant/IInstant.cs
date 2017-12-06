namespace CardProject.Cards.CardEffects.Instant
{
    public interface IInstant : ICardEffect
    {
        void Trigger(OwnedCard card);
    }
}