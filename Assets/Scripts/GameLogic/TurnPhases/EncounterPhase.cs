using CardProject.Cards;
using CardProject.Cards.CardManagers;
using CardProject.Cards.CardTypes.EncounterCardTypes;
using CardProject.Cards.CardTypes.PlayerCardTypes;
using CardProject.PlayerData;
using System.Collections.Generic;

namespace CardProject.GameLogic.TurnPhases
{
    public class EncounterPhase : InteractiveTurnPhase
    {
        private OwnedEncounterCard card;

        public EncounterPhase(List<Player> players) : base(players)
        {
        }

        public override bool IsCardPlayable(OwnedPlayerCard card)
        {
            return card.Owner == CurrentPlayer && card.PlayerCard.Type is CombatCardType;
        }

        protected override void OnPhaseStart()
        {
            base.OnPhaseStart();

            card = EncounterCardManager.Instance.InstantiateRandomCard();
            card.Owner = CurrentPlayer;
            card.gameObject.transform.position = CurrentPlayer.EncounterCardPosition.transform.position;
            card.Show();
        }

        protected override void OnPhaseEnd()
        {
            base.OnPhaseEnd();

            if (card.EncounterCard.Type is WorldCardType)
            {
                var type = card.EncounterCard.Type as WorldCardType;
                var attackDiff = type.Attack - CurrentPlayer.GetAttack();

                if (attackDiff <= 0)
                    card.Boon();
                else
                {
                    card.Burden();
                    CurrentPlayer.TakeDamage(attackDiff);
                }
            }

            card.Destroy();
            card = null;

            foreach (var player in players)
                player.ResetAttack();
        }

        protected override string GetPhaseText()
        {
            return "Encounter phase";
        }
    }
}