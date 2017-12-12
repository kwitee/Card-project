using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instant
{
    public class AddAction : IQuantifiable
    {
        public int ActionDelta;

        public void Trigger(TriggerArgs args)
        {
            args.Player.AddAction(ActionDelta);
        }

        public string GetText()
        {
            return string.Format("Action {0}.", ActionDelta.ToStringWithPlus());
        }

        public void Trigger(TriggerArgs args, int quantity)
        {
            ActionDelta = quantity;
            Trigger(args);
        }
    }
}