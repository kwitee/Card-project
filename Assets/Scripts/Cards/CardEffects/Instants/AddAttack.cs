using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instants
{
    public class AddAttack : IQuantifiable
    {
        public int AttackDelta;

        public void Trigger(TriggerArgs args)
        {
            args.Player.AddAttack(AttackDelta);
        }

        public string GetText()
        {
            return string.Format("Attack {0}.", AttackDelta.ToStringWithPlus());
        }

        public void Trigger(TriggerArgs args, int quantity)
        {
            AttackDelta = quantity;
            Trigger(args);
        }
    }
}