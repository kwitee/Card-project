namespace CardProject.CardEffects.Instant
{
    public class AddAction : IQuantifiable
    {
        public int ActionDelta;

        public void Trigger(OwnedCard card)
        {
            card.Owner.AddAction(ActionDelta);
        }

        public string GetText()
        {
            return string.Format("Action {0}.", ActionDelta.ToStringWithPlus());
        }

        public void Trigger(OwnedCard card, int quantity)
        {
            ActionDelta = quantity;
            Trigger(card);
        }
    }
}