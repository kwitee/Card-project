using CardProject.Cards;
using CardProject.Cards.CardTypes.PlayerCardTypes;
using CardProject.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CardProject.PlayerData
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private int maxHealth = 30;

        [SerializeField]
        private int maxAction = 10;

        [SerializeField]
        private int maxLearning = 10;

        [SerializeField]
        private int maxStarvation = 10;

        [SerializeField]
        private int maxAttack = 10;

        [SerializeField]
        private int minHealth = 0;

        [SerializeField]
        private int minAction = 0;

        [SerializeField]
        private int minLearning = 0;

        [SerializeField]
        private int minStarvation = 0;

        [SerializeField]
        private int resetAction = 0;

        [SerializeField]
        private int resetLearning = 0;

        [SerializeField]
        private int resetStarvation = 0;

        [SerializeField]
        private int resetAttack = 0;

        private int currentAction = 0;
        private int currentLearning = 0;
        private int currentHealth = 0;
        private int currentStarvation = 0;
        private int currentAttack = 0;

        private bool cardStatisticsShowing;
        private bool showTypeStatistics = true;
        private List<string> cardsPlayed = new List<string>();

        public PlayerHand Hand;
        public PlayerDeck Deck;
        public LearningPool LearningPool;
        public Transform CardShowPosition;
        public Transform EncounterCardPosition;

        public IEnumerable<PlayerCardType> DrawCards(int numberOfCards, string cardType = null)
        {
            return Hand.Draw(numberOfCards, Deck, cardType);
        }

        private void AddValueWithMinMaxCheck(ref int value, int delta, int min, int max)
        {
            value += delta;

            if (value > max)
                value = max;

            if (value < min)
                value = min;
        }

        public void AddLearning(int delta)
        {
            AddValueWithMinMaxCheck(ref currentLearning, delta, minLearning, maxLearning);
            UpdateGUI();
            LearningPool.RefreshHighlight();
        }

        public void ResetLearning()
        {
            currentLearning = resetLearning;
            UpdateGUI();
            LearningPool.RefreshHighlight();
        }

        public bool CanLearn(int learningCost)
        {
            return currentLearning >= learningCost;
        }

        public void AddStarvation(int delta)
        {
            AddValueWithMinMaxCheck(ref currentStarvation, delta, minStarvation, maxStarvation);
            UpdateGUI();
        }

        public void ResetStarvation()
        {
            currentStarvation = resetStarvation;
            UpdateGUI();
        }

        public void TakeStarvationDamage()
        {
            AddValueWithMinMaxCheck(ref currentHealth, -currentStarvation, minHealth, maxHealth);
            UpdateGUI();
        }

        public void TakeDamage(int damage)
        {
            AddValueWithMinMaxCheck(ref currentHealth, -damage, minHealth, maxHealth);
            UpdateGUI();
        }

        public void AddAttack(int delta)
        {
            AddValueWithMinMaxCheck(ref currentAttack, delta, int.MinValue, maxAttack);
            UpdateGUI();
        }

        public void ResetAttack()
        {
            currentAttack = resetAttack;
            UpdateGUI();
        }

        public int GetAttack()
        {
            return currentAttack;
        }

        public void AddAction(int delta)
        {
            AddValueWithMinMaxCheck(ref currentAction, delta, minAction, maxAction);
            UpdateGUI();
            Hand.RefreshHighlight();
        }

        public void ResetAction()
        {
            currentAction = resetAction;
            UpdateGUI();
            Hand.RefreshHighlight();
        }

        public bool CanPlayAction(int actionCost)
        {
            return currentAction >= actionCost;
        }

        public void Start()
        {
            currentHealth = maxHealth;
            UpdateGUI(false);
        }

        private void UpdateGUI(bool showChange = true)
        {
            GuiManager.Instance.ChangePlayerStats(showChange, currentHealth, currentLearning, currentAction, currentStarvation, currentAttack);
        }

        public void ChangeStatisticsType()
        {
            showTypeStatistics = !showTypeStatistics;
            HideCardStatistics();
            ShowCardStatistics();
        }

        public void ShowCardStatistics()
        {
            if (!cardStatisticsShowing)
            {
                cardStatisticsShowing = true;
                var allCards = GetAllCards();
                var onePercentage = allCards.Count() / 100.0;

                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(string.Format("Card count: {0}", allCards.Count));

                if (showTypeStatistics)
                    WriteStatistics(stringBuilder, allCards, onePercentage, card => card.PlayerCard.Type.GetTypeText());
                else
                    WriteStatistics(stringBuilder, allCards, onePercentage, card => card.PlayerCard.Type.Title);

                GuiManager.Instance.ShowPlayerStatistics(stringBuilder.ToString());
            }
        }

        private void WriteStatistics(StringBuilder stringBuilder, List<OwnedPlayerCard> allCards, double onePercentage, Func<OwnedPlayerCard, string> groupSelector)
        {
            var groups = allCards.GroupBy(groupSelector).Select(group => new { Text = group.Key, Count = group.Count(), DrawChance = group.Count() / onePercentage }).OrderByDescending(Text => Text.Count);

            foreach (var group in groups)
                stringBuilder.AppendLine(string.Format("{0} ({1}): {2:0.0}%", group.Text, group.Count, group.DrawChance));
        }

        public int GetNumberOfAllCards()
        {
            return GetAllCards().Count;
        }

        private List<OwnedPlayerCard> GetAllCards()
        {
            var list = new List<OwnedPlayerCard>();
            list.AddRange(Hand.GetCards());
            list.AddRange(Deck.GetCards());
            return list;
        }

        public void HideCardStatistics()
        {
            cardStatisticsShowing = false;
            GuiManager.Instance.HidePlayerStatistics();
        }

        public void AddCardPlayed(string cardTitle)
        {
            cardsPlayed.Add(cardTitle);
        }

        public void ClearCardsPlayed()
        {
            cardsPlayed.Clear();
        }

        public bool IsCardCombo(string cardTitle)
        {
            return cardsPlayed.Contains(cardTitle);
        }

        public int GetNumberOfPlayedCards()
        {
            return cardsPlayed.Count();
        }
    }
}