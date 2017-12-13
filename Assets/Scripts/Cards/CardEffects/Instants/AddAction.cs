using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instants
{
    public class AddAction : IQuantifiable
    {
        public int ActionDelta;

        public void Trigger(InstantTriggerArgs args)
        {
            args.Player.AddAction(ActionDelta);
        }

        public string GetText()
        {
            return string.Format("Action {0}.", ActionDelta.ToStringWithPlus());
        }

        public void Trigger(InstantTriggerArgs args, int quantity)
        {
            ActionDelta = quantity;
            Trigger(args);
        }
    }
}