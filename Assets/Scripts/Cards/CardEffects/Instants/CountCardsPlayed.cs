﻿using System;

namespace CardProject.Cards.CardEffects.Instants
{
    public class CountCardsPlayed : ICountable
    {
        public string GetText()
        {
            throw new NotSupportedException();
        }

        public void Trigger(InstantTriggerArgs args)
        {
            throw new NotSupportedException();
        }

        public int TriggerWithCount(InstantTriggerArgs args)
        {
            return args.Player.GetNumberOfPlayedCards();
        }
    }
}