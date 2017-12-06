using CardProject.PlayerData;
using System.Collections.Generic;

namespace CardProject.GameLogic.TurnPhases
{
    public abstract class PassiveTurnPhase : TurnPhase
    {
        public PassiveTurnPhase(List<Player> players) : base(players)
        {
        }

        public override TurnPhase UpdatePhase()
        {
            return null;
        }
    }
}