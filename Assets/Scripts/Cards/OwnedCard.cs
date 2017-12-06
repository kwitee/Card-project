using CardProject.Cards.CardEffects;
using CardProject.Cards.CardEffects.Instant;
using System.Collections.Generic;
using UnityEngine;

public class OwnedCard : MonoBehaviour
{
    public Player Owner;
    protected bool destroyed;

    public virtual void Destroy()
    {
        destroyed = true;
        Destroy(gameObject);
    }

    protected void ExecuteEffects(IEnumerable<XmlAnything<ICardEffect>> effects)
    {
        foreach (var effect in effects)
        {
            var effectValue = effect.Value;

            if (effectValue is IInstant)
                (effectValue as IInstant).Trigger(this);
        }
    }
}