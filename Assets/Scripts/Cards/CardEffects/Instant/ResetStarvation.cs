namespace CardProject.Cards.CardEffects.Instant
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