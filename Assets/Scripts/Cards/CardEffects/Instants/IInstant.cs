using CardProject.PlayerData;
using System;

namespace CardProject.Cards.CardEffects.Instants
{
    public interface IInstant : ICardEffect
    {
        void Trigger(InstantTriggerArgs args);
    }

    public class InstantTriggerArgs
    {
        /// <summary>
        /// Player that triggered the event. Cannot be null. 
        /// </summary>
        public Player Player { get; private set; }

        /// <summary>
        /// Owned card that triggered the effect. Can be null!
        /// </summary>
        public OwnedCard Card { get; private set; }
        
        public InstantTriggerArgs(Player player, OwnedCard card = null)
        {
            if (player == null)
                throw new ArgumentException("player cannot be null!");

            Player = player;
            Card = card;
        }
    }
}