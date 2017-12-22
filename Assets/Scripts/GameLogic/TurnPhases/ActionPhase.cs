using CardProject.Cards;
using CardProject.Cards.CardTypes.PlayerCardTypes;
using CardProject.PlayerData;
using System.Collections.Generic;

namespace CardProject.GameLogic.TurnPhases
{
    public class ActionPhase : InteractiveTurnPhase
    {
        public int PhaseNumber { get; set; }

        public ActionPhase(List<Player> players) : base(players)
        {
        }

        public override bool IsCardPlayable(OwnedPlayerCard card)
        {
            if (card.Owner == CurrentPlayer)
            {
                var cardTypeType = card.PlayerCard.Type.GetType();
                return cardTypeType.IsSubclassOf(typeof(ActionCardType)) || typeof(ActionCardType) == cardTypeType;
            }

            return false;
        }

        protected override void OnPhaseStart()
        {
            base.OnPhaseStart();

            foreach (var player in players)
            {
                player.AddAction(1);
                player.LearningPool.Show();
            }
        }

        protected override void OnPhaseEnd()
        {
            base.OnPhaseEnd();

            foreach (var player in players)
            {
                player.ResetAction();
                player.LearningPool.Hide();
            }
        }

        public override bool CanLearn()
        {
            return true;
        }

        protected override string GetPhaseText()
        {
            return string.Format("Action phase {0}", PhaseNumber);
        }
    }
}