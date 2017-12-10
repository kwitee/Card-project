using CardProject.Cards;
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
            var drownCards = new List<OwnedPlayerCard>();

            foreach (var card in deck.Draw(number, cardType))
            {
                card.ExecuteDrawEffects();
                AddCard(card);
                drownCards.Add(card);
            }

            RefreshHand(drownCards);
            return drownCards.Select(card => card.PlayerCard.Type);
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

                var randomCard = discardOptions[Random.Range(0, discardOptions.Count)];
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

        private Vector3 HandVector
        {
            get { return endPoint.position - startPoint.position; }
        }

        private Vector3 HandDirection
        {
            get { return HandVector.normalized; }
        }

        public Quaternion GetHandQueaternion()
        {
            var perpHandDirection = Vector3.Cross(HandDirection, Vector3.back);
            return Quaternion.FromToRotation(Vector3.up, perpHandDirection);
        }

        public void RefreshHand(IEnumerable<OwnedPlayerCard> drownCards = null)
        {
            var maxHandWidth = HandVector.magnitude;
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
                newPosition += HandDirection * (mostLeft + (PlayerCard.Width * i) - (cardMargin * i));
                newPosition += Vector3.forward * i * cardFrontMargin;                

                if (collection[i].Selected)
                    newPosition += Vector3.forward * highlightAmountFront + Vector3.up * highlightAmountUp;

                var drownCard = (drownCards != null && drownCards.Contains(collection[i]));
                AnimationQueue.Instance.AddAnimation(new Cards.Animation(collection[i].gameObject, newPosition, drownCard, false, !drownCard, !drownCard));
            }
        }
    }
}