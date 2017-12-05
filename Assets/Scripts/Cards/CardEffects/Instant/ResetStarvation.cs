namespace CardProject.CardEffects.Instant
{
    public class ResetStarvation : IInstant
    {
        public void Trigger(OwnedCard card)
        {
            card.Owner.ResetStarvation();
        }

        public string GetText()
        {
            return string.Format("Starvation reset.");
        }
    }
}