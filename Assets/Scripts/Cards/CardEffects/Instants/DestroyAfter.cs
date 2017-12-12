using System;

namespace CardProject.Cards.CardEffects.Instants
{
    public class DestroyAfter : IInstant
    {
        public void Trigger(TriggerArgs args)
        {
            if (args.Card == null)
                throw new ArgumentException("args doesn't contain reference for a card!");

            args.Card.Destroy();
        }

        public string GetText()
        {
            return string.Format("Destroy this card.");
        }
    }
}