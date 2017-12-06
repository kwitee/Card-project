using CardProject.PlayerData;
using System.Collections.Generic;
using UnityEngine;

namespace CardProject.GameLogic.TurnPhases
{
    public abstract class NonInteractiveTurnPhase : TurnPhase
    {
        protected float phaseStarted;
        protected float phaseDelay = 2f;

        public NonInteractiveTurnPhase(List<Player> players) : base(players)
        {
        }

        protected override void OnPhaseStart()
        {
            base.OnPhaseStart();
            phaseStarted = Time.time;
        }

        public override TurnPhase UpdatePhase()
        {
            var phaseTime = Time.time - phaseStarted;

            if (phaseTime > phaseDelay)
                return NextPhase;
            else
                return null;
        }
    }
}