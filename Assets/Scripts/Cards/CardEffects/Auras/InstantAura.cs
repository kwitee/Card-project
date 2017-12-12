using CardProject.Cards.CardEffects.Instants;
using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Auras
{
    public class InstantAura : Aura
    {
        public XmlAnything<IInstant> Effect;

        public override void Trigger(OwnedPlayerCard card)
        {
            base.Trigger(card);
            Effect.Value.Trigger(new TriggerArgs(Player));
        }
    }
}