using CardProject.Gui;
using CardProject.PlayerData;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.GameLogic.TurnPhases
{
    public abstract class InteractiveTurnPhase : TurnPhase
    {
        public Player CurrentPlayer { get; protected set; }
        protected bool nextPhaseReady;

        public InteractiveTurnPhase(List<Player> players) : base(players)
        {
            CurrentPlayer = players.First();
            nextPhaseReady = false;
        }

        protected override void OnPhaseStart()
        {
            base.OnPhaseStart();
            nextPhaseReady = false;
            GuiManager.Instance.ShowEndPhaseButton();
        }

        protected override void OnPhaseEnd()
        {
            base.OnPhaseEnd();
            GuiManager.Instance.HideEndPhaseButton();

            foreach (var player in players)
                player.ClearCardsPlayed();
        }

        public void AdvancePhase()
        {
            nextPhaseReady = true;
        }

        public override TurnPhase UpdatePhase()
        {
            return nextPhaseReady ? NextPhase : null;
        }
    }
}