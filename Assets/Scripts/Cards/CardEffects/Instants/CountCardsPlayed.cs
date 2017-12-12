using System;

namespace CardProject.Cards.CardEffects.Instants
{
    public class CountCardsPlayed : ICountable
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
            return args.Player.GetNumberOfPlayedCards();
        }
    }
}