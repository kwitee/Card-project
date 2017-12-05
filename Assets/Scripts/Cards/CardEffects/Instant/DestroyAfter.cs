namespace CardProject.CardEffects.Instant
{
    public class DestroyAfter : IInstant
    {
        public void Trigger(OwnedCard card)
        {
            card.Destroy();
        }

        public string GetText()
        {
            return string.Format("Destroy this card.");
        }
    }
}