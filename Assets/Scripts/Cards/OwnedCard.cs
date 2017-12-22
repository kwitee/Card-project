using CardProject.Cards.CardEffects;
using CardProject.Cards.CardEffects.Auras;
using CardProject.Cards.CardEffects.Instants;
using CardProject.Helpers;
using CardProject.PlayerData;
using System.Collections.Generic;
using UnityEngine;

namespace CardProject.Cards
{
    public class OwnedCard : MonoBehaviour
    {
        public Player Owner { get; set; }
        protected bool destroyed;

        public virtual void Destroy()
        {
            destroyed = true;            
        }

        protected void ExecuteEffects(IEnumerable<XmlAnything<ICardEffect>> effects)
        {
            foreach (var effect in effects)
            {
                var effectValue = effect.Value;

                if (effectValue is IInstant)
                    (effectValue as IInstant).Trigger(new InstantTriggerArgs(Owner, this));

                if (effectValue is Aura)
                    (effectValue as Aura).Register(Owner);
            }
        }
    }
}