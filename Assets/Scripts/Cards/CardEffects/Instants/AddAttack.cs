using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instants
{
    public class AddAttack : IQuantifiable
    {
        public int AttackDelta;

        public void Trigger(InstantTriggerArgs args)
        {
            args.Player.AddAttack(AttackDelta);
        }

        public string GetText()
        {
            return string.Format("Attack {0}.", AttackDelta.ToStringWithPlus());
        }

        public void Trigger(InstantTriggerArgs args, int quantity)
        {
            AttackDelta = quantity;
            Trigger(args);
        }
    }
}