using CardProject.Cards;
using CardProject.PlayerData;
using System.Collections.Generic;

namespace CardProject.GameLogic.TurnPhases
{
    public class DrawPhase : NonInteractiveTurnPhase
    {
        public DrawPhase(List<Player> players) : base(players)
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
                player.DrawCards(5);
                player.AddStarvation(1);
            }
        }

        protected override string GetPhaseText()
        {
            return "Draw phase";
        }
    }
}