namespace CardProject.Cards.CardEffects.Instants
{
    public interface IQuantifiable : IInstant
    {
        void Trigger(InstantTriggerArgs args, int quantity);
    }
}