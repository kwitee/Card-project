using CardProject.Cards.CardManagers;
using CardProject.Cards.CardTypes.PlayerCardTypes;
using CardProject.GameLogic;
using CardProject.Gui;
using CardProject.Helpers;
using System.Linq;
using UnityEngine;

namespace CardProject.Cards
{
    public class LearningPool : Singleton<LearningPool>
    {
        [SerializeField]
        private float scrollVelocity = 4f;

        private bool visible = false;

        public void Start()
        {
            int i = 0;

            foreach (var card in PlayerCardManager.Instance.InstantiateLearningPoolCards(CardSet.Basic).OrderBy(a => a.PlayerCard.Type.GetTypeText()).ThenBy(card => card.PlayerCard.Type.LearningCost)
                .ThenBy(card => card.PlayerCard.Type.Title))
            {
                card.transform.position = gameObject.transform.position + Vector3.right * i * PlayerCard.Width;
                card.transform.parent = transform;
                card.Hide();
                i++;
            }
        }

        public void LearnCard(PlayerCardType type)
        {
            if (GameManager.Instance.CanLearn())
            {
                var player = GameManager.Instance.GetCurrentPlayer();

                if ((player != null) && player.CanLearn(type.LearningCost))
                {
                    player.Deck.AddNewCard(type.Title, 1);
                    player.AddLearning(-type.LearningCost);
                }
                else
                    GuiManager.Instance.ShowFadeOutText("You don't have enough Learning!");
            }
            else
                GuiManager.Instance.ShowFadeOutText("Card's not learnable now!");
        }

        public void Update()
        {
            if (visible)
            {
                var deltaScroll = Input.mouseScrollDelta.y * scrollVelocity;

                foreach (var card in GetComponentsInChildren<LearningPoolPlayerCard>())
                    card.Move(Vector3.right * deltaScroll);
            }
        }

        public void Show()
        {
            visible = true;

            foreach (var card in GetComponentsInChildren<LearningPoolPlayerCard>(true))
                card.Show();
        }

        public void Hide()
        {
            visible = false;

            foreach (var card in GetComponentsInChildren<LearningPoolPlayerCard>())
                card.Hide();
        }
    }
}