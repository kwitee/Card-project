using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instants
{
    public class AddStarvation : IQuantifiable
    {
        public int StarvationDelta;

        public void Trigger(InstantTriggerArgs args)
        {
            args.Player.AddStarvation(StarvationDelta);
        }

        public string GetText()
        {
            return string.Format("Starvation {0}.", StarvationDelta.ToStringWithPlus());
        }

        public void Trigger(InstantTriggerArgs args, int quantity)
        {
            StarvationDelta = quantity;
            Trigger(args);
        }
    }
}