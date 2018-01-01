using CardProject.Cards.CardManagers;
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
        private List<LearningPoolPlayerCard> collection;
        public Player Player { get; private set; }

        public void Awake()
        {
            collection = new List<LearningPoolPlayerCard>();
            Player = GetComponentInParent<Player>();
        }

        public void Start()
        {
            int i = 0;

            foreach (var card in PlayerCardManager.Instance.InstantiateLearningPoolCards(CardSet.Basic).OrderBy(a => a.PlayerCard.Type.GetTypeText()).ThenBy(card => card.PlayerCard.Type.LearningCost)
                .ThenBy(card => card.PlayerCard.Type.Title))
            {
                card.transform.position = centerPoint.position + Player.Hand.HandDirection * (-halfLength) + Player.Hand.HandDirection * i * PlayerCard.Width;
                card.LearningPool = this;
                card.transform.parent = transform;
                card.gameObject.transform.rotation = Player.Hand.GetHandQueaternion();
                card.Hide();
                collection.Add(card);
                i++;
            }
        }

        public void Update()
        {
            if (visible)
            {
                var deltaScroll = Input.mouseScrollDelta.y * PlayerCard.Width;

                if (StopMoving(collection, deltaScroll))
                    return;

                foreach (var card in collection)
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
                foreach (var card in collection)
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
            return futureFirstCardPosition + Player.Hand.HandDirection * deltaScroll;
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

        public void RefreshHighlight()
        {
            foreach (var card in collection)
                card.PlayerCard.ChangeHighlightLearnable(card.IsCardLearnable());
        }
    }
}