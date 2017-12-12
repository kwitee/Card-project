namespace CardProject.Cards.CardEffects.Instants
{
    public interface IQuantifiable : IInstant
    {
        void Trigger(TriggerArgs args, int quantity);
    }
}