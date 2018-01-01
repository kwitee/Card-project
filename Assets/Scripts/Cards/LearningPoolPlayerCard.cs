using CardProject.GameLogic;
using CardProject.Gui;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardProject.Cards
{
    [RequireComponent(typeof(PlayerCard))]
    public class LearningPoolPlayerCard : MonoBehaviour
    {
        public PlayerCard PlayerCard { get; private set; }
        public LearningPool LearningPool { get; set; }

        public void Awake()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            PlayerCard = GetComponent<PlayerCard>();
            var entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { PointerClick(); });
            GetComponent<EventTrigger>().triggers.Add(entry);
        }

        private void LearnCard()
        {
            string unlearnableReason;

            if (IsCardLearnable(out unlearnableReason))
            {
                LearningPool.Player.Deck.AddNewCard(PlayerCard.Type.Title, 1);
                LearningPool.Player.AddLearning(-PlayerCard.Type.LearningCost);
            }
            else
                GuiManager.Instance.ShowFadeOutText(unlearnableReason);
        }

        private bool IsCardLearnable(out string unlearnableReason)
        {
            if (GameManager.Instance.CanLearn())
            {
                if (LearningPool.Player.CanLearn(PlayerCard.Type.LearningCost))
                {
                    unlearnableReason = string.Empty;
                    return true;
                }
                else
                    unlearnableReason = "You don't have enough Learning!";
            }
            else
                unlearnableReason = "Card's not learnable in this phase!";

            return false;
        }

        public bool IsCardLearnable()
        {
            string unlearnableReason;
            return IsCardLearnable(out unlearnableReason);
        }

        public void PointerClick()
        {
            LearnCard();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public bool IsVisible()
        {
            return gameObject.activeInHierarchy;
        }

        public void Move(Vector3 position)
        {
            transform.position = position;
        }
    }
}