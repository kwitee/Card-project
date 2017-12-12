using CardProject.Cards;
using CardProject.Cards.CardEffects.Auras;
using CardProject.Gui;
using CardProject.PlayerData;
using System.Collections.Generic;

namespace CardProject.GameLogic.TurnPhases
{
    public abstract class TurnPhase
    {
        protected List<Player> players;

        public TurnPhase NextPhase { get; set; }
        public bool Active { get; private set; }

        public void ActivatePhase()
        {
            Active = true;
            OnPhaseStart();
        }

        public void InactivatePhase()
        {
            Active = false;
            OnPhaseEnd();
        }

        public abstract bool IsCardPlayable(OwnedPlayerCard card);
        public abstract TurnPhase UpdatePhase();

        protected TurnPhase(List<Player> players)
        {
            this.players = players;
        }

        protected abstract string GetPhaseText();

        protected virtual void OnPhaseStart()
        {
            GuiManager.Instance.ChangePhaseText(GetPhaseText());
        }

        protected virtual void OnPhaseEnd()
        {
            AuraCollection.Instance.PhaseEndUnregister();
        }

        public virtual bool CanLearn()
        {
            return false;
        }
    }
}