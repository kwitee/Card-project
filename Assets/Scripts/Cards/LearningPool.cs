using CardProject.Cards.CardManagers;
using CardProject.Cards.CardTypes.PlayerCardTypes;
using CardProject.GameLogic;
using CardProject.Gui;
using CardProject.PlayerData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardProject.Cards
{
    public class LearningPool : MonoBehaviour
    {
        [SerializeField]
        private Transform centerPoint = null;

        [SerializeField]
        private float halfLength = 24f;

        private bool visible = false;
        private Player player;

        public void Awake()
        {
            player = GetComponentInParent<Player>();
        }

        public void Start()
        {
            int i = 0;

            foreach (var card in PlayerCardManager.Instance.InstantiateLearningPoolCards(CardSet.Basic).OrderBy(a => a.PlayerCard.Type.GetTypeText()).ThenBy(card => card.PlayerCard.Type.LearningCost)
                .ThenBy(card => card.PlayerCard.Type.Title))
            {
                card.transform.position = centerPoint.position + player.Hand.HandDirection * (-halfLength) + player.Hand.HandDirection * i * PlayerCard.Width;
                card.LearningPool = this;
                card.transform.parent = transform;
                card.gameObject.transform.rotation = player.Hand.GetHandQueaternion();
                card.Hide();
                i++;
            }
        }

        public void LearnCard(PlayerCardType type)
        {
            if (GameManager.Instance.CanLearn())
            {
                if (player.CanLearn(type.LearningCost))
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
                var deltaScroll = Input.mouseScrollDelta.y * PlayerCard.Width;
                var cards = GetComponentsInChildren<LearningPoolPlayerCard>(true).ToList();

                if (StopMoving(cards, deltaScroll))
                    return;

                foreach (var card in cards)
                {
                    var futureCardPosition = GetFutureCardPosition(card, deltaScroll);
                    card.Move(futureCardPosition);

                    if (IsCardOutOfBounds(futureCardPosition))
                        card.Hide();
                    else
                        card.Show();
                }
            }
            else
            {
                foreach (var card in GetComponentsInChildren<LearningPoolPlayerCard>())
                    card.Hide();
            }
        }

        private bool StopMoving(List<LearningPoolPlayerCard> cards, float deltaScroll)
        {
            var firstCard = cards.First();
            var lastCard = cards.Last();
            var futureFirstCardPosition = GetFutureCardPosition(firstCard, deltaScroll);
            var futureLastCardPosition = GetFutureCardPosition(lastCard, deltaScroll);
            return (firstCard.IsVisible() && IsCardOutOfBounds(futureFirstCardPosition) && deltaScroll > 0) || (lastCard.IsVisible() && IsCardOutOfBounds(futureLastCardPosition) && deltaScroll < 0);
        }

        private Vector3 GetFutureCardPosition(LearningPoolPlayerCard card, float deltaScroll)
        {
            var futureFirstCardPosition = card.transform.position;
            return futureFirstCardPosition + player.Hand.HandDirection * deltaScroll;
        }

        private bool IsCardOutOfBounds(Vector3 futureFirstCardPosition)
        {
            return Vector3.Distance(futureFirstCardPosition, centerPoint.position) > halfLength;
        }

        public void Show()
        {
            visible = true;
        }

        public void Hide()
        {
            visible = false;
        }
    }
}
 