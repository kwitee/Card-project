using CardProject.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Auras
{
    public class AuraCollection : Singleton<AuraCollection>
    {
        private List<Aura> collection = new List<Aura>();

        public void Register(Aura aura)
        {
            collection.Add(aura);
        }

        public void Unregister(Aura aura)
        {
            collection.Remove(aura);
        }

        public void TriggerEffects(TriggerEvent triggerEvent, OwnedPlayerCard card)
        {
            foreach (var aura in collection.Where(aura => aura.Player == card.Owner && aura.TriggerEvent == triggerEvent).ToList())
                aura.Trigger(new AuraTriggerArgs(card));
        }

        public void PhaseEndUnregister()
        {
            collection.RemoveAll(aura => aura.AutoUnregister == AutoUnregister.PhaseEnd);
        }

        public void TurnEndUnregister()
        {
            collection.RemoveAll(aura => aura.AutoUnregister == AutoUnregister.TurnEnd);
        }
    }
}