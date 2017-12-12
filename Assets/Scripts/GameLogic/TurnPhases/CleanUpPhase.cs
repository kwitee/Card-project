using CardProject.Cards;
using CardProject.Cards.CardEffects.Auras;
using CardProject.PlayerData;
using System.Collections.Generic;

namespace CardProject.GameLogic.TurnPhases
{
    public class CleanUpPhase : NonInteractiveTurnPhase
    {
        public CleanUpPhase(List<Player> players) : base(players)
        {
        }

        public override bool IsCardPlayable(OwnedPlayerCard card)
        {
            return false;
        }

        protected override void OnPhaseStart()
        {
            base.OnPhaseStart();

            foreach (var player in players)
            {
                player.Hand.DiscardAllCards();
                player.TakeStarvationDamage();
                player.ResetLearning();
            }
        }

        protected override void OnPhaseEnd()
        {
            base.OnPhaseEnd();
            GameManager.Instance.IncrementTurnCounter();
            GameManager.Instance.ChangeFirstPlayer();
            AuraCollection.Instance.TurnEndUnregister();
        }

        protected override string GetPhaseText()
        {
            return "Clean up phase";
        }
    }
}