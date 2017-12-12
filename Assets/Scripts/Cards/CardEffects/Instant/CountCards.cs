using System;

namespace CardProject.Cards.CardEffects.Instant
{
    public class CountCards : ICountable
    {
        public string GetText()
        {
            throw new NotSupportedException();
        }

        public void Trigger(TriggerArgs args)
        {
            throw new NotSupportedException();
        }

        public int TriggerWithCount(TriggerArgs args)
        {
            return args.Player.GetNumberOfAllCards();
        }
    }
}