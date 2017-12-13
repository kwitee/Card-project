namespace CardProject.Cards.CardEffects.Instants
{
    public class TakeDamage : IQuantifiable
    {
        public int Damage;

        public void Trigger(InstantTriggerArgs args)
        {
            args.Player.TakeDamage(Damage);
        }

        public string GetText()
        {
            if (Damage >= 0)
                return string.Format("Take {0} damage.", Damage);
            else
                return string.Format("Heal {0}.", -Damage);
        }

        public void Trigger(InstantTriggerArgs args, int quantity)
        {
            Damage = quantity;
            Trigger(args);
        }
    }
}