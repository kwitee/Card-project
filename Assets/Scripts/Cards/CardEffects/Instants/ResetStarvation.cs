namespace CardProject.Cards.CardEffects.Instants
{
    public class ResetStarvation : IInstant
    {
        public void Trigger(TriggerArgs args)
        {
            args.Player.ResetStarvation();
        }

        public string GetText()
        {
            return string.Format("Starvation reset.");
        }
    }
}