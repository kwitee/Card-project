using CardProject.GameLogic;
using CardProject.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Gui
{
    public sealed class GuiManager : Singleton<GuiManager>
    {
        [SerializeField]
        private Text turnCounterText = null;

        [SerializeField]
        private Text tierText = null;

        [SerializeField]
        private Text phaseText = null;

        [SerializeField]
        private Button endPhaseButton = null;

        [SerializeField]
        private Text healthText = null;

        [SerializeField]
        private Text learningText = null;

        [SerializeField]
        private Text actionText = null;

        [SerializeField]
        private Text starvationText = null;

        [SerializeField]
        private Text attackText = null;

        [SerializeField]
        private GameObject playerStatisticsPanel = null;

        [SerializeField]
        private Text playerStatisticsText = null;

        [SerializeField]
        private GameObject fadeOutTextPrefab = null;

        [SerializeField]
        private float showTextWait = 0.65f;

        [SerializeField]
        private Canvas canvas = null;

        private int currentHealth = 0;
        private int currentLearning = 0;
        private int currentAction = 0;
        private int currentStarvation = 0;
        private int currentAttack = 0;
        private List<string> fadeOutTexts = new List<string>();

        public void ChangeTurnCounterText(int turnCounter)
        {
            turnCounterText.text = turnCounter.ToString();
        }

        public void ChangeTierText(int tier)
        {
            tierText.text = tier.ToString();
        }

        public void ChangePhaseText(string text)
        {
            phaseText.text = text;
        }

        public void ShowEndPhaseButton()
        {
            endPhaseButton.gameObject.SetActive(true);
        }

        public void HideEndPhaseButton()
        {
            endPhaseButton.gameObject.SetActive(false);
        }

        public void ChangePlayerStats(bool showChange, int health, int learning, int action, int starvation, int attack)
        {
            ChangePlayerStat(ref currentHealth, health, healthText, "Health", showChange);
            ChangePlayerStat(ref currentLearning, learning, learningText, "Learning", showChange);
            ChangePlayerStat(ref currentAction, action, actionText, "Action", showChange);
            ChangePlayerStat(ref currentStarvation, starvation, starvationText, "Starvation", showChange);
            ChangePlayerStat(ref currentAttack, attack, attackText, "Attack", showChange);
        }

        public void ShowFadeOutText(string text)
        {
            fadeOutTexts.Add(text);
        }

        private void ChangePlayerStat(ref int stat, int newStat, Text text, string statName, bool showChange)
        {
            text.text = newStat.ToString();

            if (stat != newStat)
            {
                if (showChange)
                    fadeOutTexts.Add(string.Format("{0} {1}", statName, (newStat - stat).ToStringWithPlus()));

                stat = newStat;
            }
        }

        public void LateUpdate()
        {
            StartCoroutine(ShowText(fadeOutTexts.ToArray()));
            fadeOutTexts.Clear();
        }

        private IEnumerator ShowText(string[] texts)
        {
            foreach (var text in texts)
            {
                FadeOutText.Instantiate(fadeOutTextPrefab, canvas, text);
                yield return new WaitForSeconds(showTextWait);
            }
        }

        public void EndPhaseButtonClick()
        {
            GameManager.Instance.EndPhaseClick();
        }

        public void ShowLearningPoolButtonClick()
        {
            var currentPlayer = GameManager.Instance.GetCurrentPlayer();

            if (currentPlayer != null)
                currentPlayer.LearningPool.Show();
        }

        public void HideLearningPoolButtonClick()
        {
            var currentPlayer = GameManager.Instance.GetCurrentPlayer();

            if (currentPlayer != null)
                currentPlayer.LearningPool.Hide();
        }

        public void ShowPlayerStatistics(string statistics)
        {
            playerStatisticsText.text = statistics;
            playerStatisticsPanel.SetActive(true);
        }

        public void HidePlayerStatistics()
        {
            playerStatisticsPanel.SetActive(false);
        }
    }
}