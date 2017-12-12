using CardProject.PlayerData;
using System;

namespace CardProject.Cards.CardEffects.Instant
{
    public interface IInstant : ICardEffect
    {
        void Trigger(TriggerArgs args);
    }

    public class TriggerArgs
    {
        /// <summary>
        /// Player that triggered the event. Cannot be null. 
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        /// Owned card that triggered the effect. Can be null!
        /// </summary>
        public OwnedCard Card { get; private set; }
        
        public TriggerArgs(Player player, OwnedCard card)
        {
            if (player == null)
                throw new ArgumentException("player cannot be null!");

            Player = player;
            Card = card;
        }
    }
}