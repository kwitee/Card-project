namespace CardProject.Cards.CardEffects.Instant
{
    public interface IQuantifiable : IInstant
    {
        void Trigger(TriggerArgs args, int quantity);
    }
}