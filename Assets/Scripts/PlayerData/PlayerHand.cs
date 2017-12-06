using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardProject.PlayerData
{
    public class PlayerHand : OwnedPlayerCardCollection
    {
        [SerializeField]
        private Transform startPoint = null;

        [SerializeField]
        private Transform endPoint = null;

        [SerializeField]
        private float cardFrontMargin = -0.1f;

        [SerializeField]
        private float highlightAmountFront = -0.3f;

        [SerializeField]
        private float highlightAmountUp = 0.4f;

        public IEnumerable<PlayerCardType> Draw(int number, PlayerDeck deck, string cardType = null)
        {
            var drownCards = new List<PlayerCardType>();

            foreach (var card in deck.Draw(number, cardType))
            {
                card.ExecuteDrawEffects();
                AddCard(card);
                drownCards.Add(card.PlayerCard.Type);
            }

            RefreshHand();
            return drownCards;
        }

        public override void RemoveCard(OwnedPlayerCard card)
        {
            base.RemoveCard(card);
            RefreshHand();
        }

        public IEnumerable<PlayerCardType> DiscardRandomCard(int number, string cardType = null)
        {
            List<OwnedPlayerCard> discardOptions;
            var discardedTypes = new List<PlayerCardType>();

            if (cardType == null)
                discardOptions = collection.ToList();
            else
                discardOptions = collection.Where(c => c.PlayerCard.Type.GetTypeText() == cardType).ToList();

            while (number > 0)
            {
                if (discardOptions.Count == 0)
                    break;

                var randomCard = discardOptions[UnityEngine.Random.Range(0, discardOptions.Count)];
                discardOptions.Remove(randomCard);
                discardedTypes.Add(randomCard.PlayerCard.Type);
                randomCard.Discard();
                number--;
            }

            return discardedTypes;
        }

        public IEnumerable<PlayerCardType> DiscardAllCards(string cardType = null)
        {
            List<OwnedPlayerCard> discardOptions;

            if (cardType == null)
                discardOptions = collection.ToList();
            else
                discardOptions = collection.Where(c => c.PlayerCard.Type.GetTypeText() == cardType).ToList();

            var discardedTypes = discardOptions.Select(card => card.PlayerCard.Type).ToList();

            foreach (var card in discardOptions)
                card.Discard();

            return discardedTypes;
        }

        public override void AddNewCard(string title, int number)
        {
            base.AddNewCard(title, number);
            RefreshHand();
        }

        public override void AddCard(OwnedPlayerCard card)
        {
            base.AddCard(card);
            card.State = OwnedPlayerCardState.InHand;
        }

        public void RefreshHand()
        {
            var handVector = endPoint.position - startPoint.position;
            var handDirection = handVector.normalized;
            var perpHandDirection = Vector3.Cross(handDirection, Vector3.back);
            var maxHandWidth = handVector.magnitude;

            var neededHandWidth = (collection.Count - 1) * PlayerCard.Width;
            var cardMargin = 0f;

            if (neededHandWidth > maxHandWidth)
            {
                cardMargin = (neededHandWidth - maxHandWidth) / (collection.Count - 1);
                neededHandWidth = maxHandWidth;
            }

            var mostLeft = (maxHandWidth / 2) - (neededHandWidth / 2);

            for (int i = 0; i < collection.Count; i++)
            {
                var newPosition = startPoint.position;
                newPosition += handDirection * (mostLeft + (PlayerCard.Width * i) - (cardMargin * i));
                newPosition += Vector3.forward * i * cardFrontMargin;

                if (collection[i].Selected)
                    newPosition += Vector3.forward * highlightAmountFront + Vector3.up * highlightAmountUp;

                collection[i].MovePlayerCard.MoveTo(newPosition);
                collection[i].gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, perpHandDirection);
            }
        }
    }
}