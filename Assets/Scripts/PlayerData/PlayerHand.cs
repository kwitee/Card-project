using CardProject.Cards;
using CardProject.Cards.CardEffects.Auras;
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
        private float selectAmountFront = -0.3f;

        [SerializeField]
        private float selectAmountUp = 0.4f;

        [SerializeField]
        private int handLimit = 10;

        public IEnumerable<PlayerCardType> Draw(int number, PlayerDeck deck, string cardType = null)
        {
            number = RestrictAddNumber(number);
            var drownCards = new List<OwnedPlayerCard>();

            foreach (var card in deck.Draw(number, cardType))
            {                
                AddCard(card);
                drownCards.Add(card);
                AuraCollection.Instance.TriggerEffects(TriggerEvent.CardDrown, card);
            }

            RefreshHand(drownCards);
            RefreshHighlight();

            foreach (var card in drownCards)            
                card.ExecuteDrawEffects();            

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
            number = RestrictAddNumber(number);

            if (number > 0)
            {
                base.AddNewCard(title, number);
                RefreshHand();
            }
        }

        public override void AddCard(OwnedPlayerCard card)
        {
            if (!HasMaximumCards())
            {
                base.AddCard(card);
                card.State = OwnedPlayerCardState.InHand;
            }
        }

        private bool HasMaximumCards()
        {
            return collection.Count >= handLimit;
        }

        private int RestrictAddNumber(int number)
        {
            if (collection.Count + number > handLimit)
                return number - (collection.Count + number - handLimit);

            return number;
        }

        private Vector3 HandVector
        {
            get { return endPoint.position - startPoint.position; }
        }

        /// <summary>
        /// Normalized vector of direction of hand card sequence. 
        /// </summary>
        public Vector3 HandDirection
        {
            get { return HandVector.normalized; }
        }

        /// <summary>
        /// Quaternion of player card rotation. 
        /// </summary>
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
                    newPosition += Vector3.forward * selectAmountFront + Vector3.up * selectAmountUp;

                var drownCard = (drownCards != null && drownCards.Contains(collection[i]));
                AnimationQueue.Instance.AddAnimation(new Cards.Animation(collection[i].gameObject, newPosition, drownCard, false, !drownCard, !drownCard));
            }
        }

        public void RefreshHighlight()
        {
            foreach (var card in collection)
                card.PlayerCard.ChangeHighlightPlayable(card.IsCardPlayable());
        }
    }
}