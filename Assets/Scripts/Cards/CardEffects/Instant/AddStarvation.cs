using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instant
{
    public class AddStarvation : IQuantifiable
    {
        public int StarvationDelta;

        public void Trigger(OwnedCard card)
        {
            card.Owner.AddStarvation(StarvationDelta);
        }

        public string GetText()
        {
            return string.Format("Starvation {0}.", StarvationDelta.ToStringWithPlus());
        }

        public void Trigger(OwnedCard card, int quantity)
        {
            StarvationDelta = quantity;
            Trigger(card);
        }
    }
}