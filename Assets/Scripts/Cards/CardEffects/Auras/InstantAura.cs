using CardProject.Cards.CardEffects.Instants;
using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Auras
{
    public class InstantAura : Aura
    {
        public XmlAnything<IInstant> Effect;

        public override void OnTrigger(AuraTriggerArgs args)
        {
            Effect.Value.Trigger(new InstantTriggerArgs(Player));
        }
    }
}