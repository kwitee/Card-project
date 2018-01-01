using CardProject.Cards;
using CardProject.Cards.CardManagers;
using CardProject.GameLogic.TurnPhases;
using CardProject.Gui;
using CardProject.Helpers;
using CardProject.PlayerData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardProject.GameLogic
{
    public sealed class GameManager : Singleton<GameManager>
    {
        private List<Player> players;
        private Player firstPlayer;
        private TurnPhase currentTurnPhase;
        private int turnCounter;
        private int tierIncrementModulo = 10;
        private int maxTier = 3;

        public int Tier { get; private set; }

        private DrawPhase drawPhase;
        private ActionPhase actionPhase;
        private EncounterPhase encounterPhase;
        private ActionPhase actionPhase2;
        private CleanUpPhase cleanUpPhase;

        public void Start()
        {
            GamePrepare();
            StartCoroutine(GameStart());
        }

        public void Update()
        {
            if (currentTurnPhase != null)
            {
                var next = currentTurnPhase.UpdatePhase();

                if (next != null)
                    ChangeCurrentTurnPhase(next);
            }
        }

        private void GamePrepare()
        {
            players = new List<Player>();
            players.AddRange(FindObjectsOfType<Player>());
            firstPlayer = players.First();

            foreach (var player in players)
            {
                player.Deck.AddNewCard("Left Hook", 4);
                player.Deck.AddNewCard("Small Fact", 6);
                player.Deck.AddNewCard("Lunchmeat Can", 2);
            }

            IncrementTier();
            IncrementTurnCounter();
            SetUpPhases();
        }

        private void SetUpPhases()
        {
            drawPhase = new DrawPhase(players);
            actionPhase = new ActionPhase(players);
            encounterPhase = new EncounterPhase(players);
            actionPhase2 = new ActionPhase(players);
            cleanUpPhase = new CleanUpPhase(players);

            drawPhase.NextPhase = actionPhase;
            actionPhase.NextPhase = encounterPhase;
            encounterPhase.NextPhase = actionPhase2;
            actionPhase2.NextPhase = cleanUpPhase;
            cleanUpPhase.NextPhase = drawPhase;

            actionPhase.PhaseNumber = 1;
            actionPhase2.PhaseNumber = 2;
        }

        private IEnumerator GameStart()
        {
            yield return new WaitForSeconds(5);
            ChangeCurrentTurnPhase(drawPhase);
        }

        private void ChangeCurrentTurnPhase(TurnPhase newPhase)
        {
            if (currentTurnPhase != null)
                currentTurnPhase.InactivatePhase();

            currentTurnPhase = newPhase;
            currentTurnPhase.ActivatePhase();

            foreach (var player in players)
            {
                player.Hand.RefreshHighlight();
                player.LearningPool.RefreshHighlight();
            }
        }

        public bool IsCardPlayable(OwnedPlayerCard card)
        {
            return currentTurnPhase.IsCardPlayable(card);
        }

        public bool CanLearn()
        {
            return currentTurnPhase.CanLearn();
        }

        public Player GetCurrentPlayer()
        {
            if (currentTurnPhase is InteractiveTurnPhase)
                return (currentTurnPhase as InteractiveTurnPhase).CurrentPlayer;
            else
                return null;
        }

        public void ChangeFirstPlayer()
        {
            players.Remove(firstPlayer);
            players.Add(firstPlayer);
            firstPlayer = players.First();
        }

        public void EndPhaseClick()
        {
            if (currentTurnPhase is InteractiveTurnPhase)
                (currentTurnPhase as InteractiveTurnPhase).AdvancePhase();
        }

        public void IncrementTurnCounter()
        {
            GuiManager.Instance.ChangeTurnCounterText(++turnCounter);

            if ((turnCounter % tierIncrementModulo) == 0)
                IncrementTier();
        }

        private void IncrementTier()
        {
            if (Tier <= maxTier)
            {
                GuiManager.Instance.ChangeTierText(++Tier);
                EncounterCardManager.Instance.UpdateEncounterDeck();
            }
        }
    }
}