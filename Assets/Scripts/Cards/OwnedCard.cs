using CardProject.Cards.CardEffects;
using CardProject.Cards.CardEffects.Instants;
using CardProject.Helpers;
using CardProject.PlayerData;
using System.Collections.Generic;
using UnityEngine;

namespace CardProject.Cards
{
    public class OwnedCard : MonoBehaviour
    {
        public Player Owner;
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
                    (effectValue as IInstant).Trigger(new TriggerArgs(Owner, this));
            }
        }
    }
}