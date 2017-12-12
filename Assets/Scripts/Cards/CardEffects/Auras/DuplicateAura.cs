namespace CardProject.Cards.CardEffects.Auras
{
    public class DuplicateAura : Aura
    {
        public override void Trigger(OwnedPlayerCard card)
        {
            base.Trigger(card);
            card.ExecutePlayEffects();
        }
    }
}