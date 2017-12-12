﻿using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instant
{
    public class AddStarvation : IQuantifiable
    {
        public int StarvationDelta;

        public void Trigger(TriggerArgs args)
        {
            args.Player.AddStarvation(StarvationDelta);
        }

        public string GetText()
        {
            return string.Format("Starvation {0}.", StarvationDelta.ToStringWithPlus());
        }

        public void Trigger(TriggerArgs args, int quantity)
        {
            StarvationDelta = quantity;
            Trigger(args);
        }
    }
}