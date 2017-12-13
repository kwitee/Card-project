namespace CardProject.Cards.CardEffects.Auras
{
    public class DuplicateAura : Aura
    {
        public override void OnTrigger(AuraTriggerArgs args)
        {
            args.Card.ExecutePlayEffects();
        }
    }
}