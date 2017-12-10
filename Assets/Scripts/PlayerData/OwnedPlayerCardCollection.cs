using CardProject.Cards;
using CardProject.Cards.CardManagers;
using CardProject.Cards.CardTypes.PlayerCardTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardProject.PlayerData
{
    public abstract class OwnedPlayerCardCollection : MonoBehaviour
    {
        protected List<OwnedPlayerCard> collection;
        protected Player player;

        public void Awake()
        {
            collection = new List<OwnedPlayerCard>();
            player = GetComponentInParent<Player>();
        }

        public virtual void AddNewCard(string title, int number)
        {
            if (number <= 0)
                throw new ArgumentException("Number cannot be less or equal of zero!", "number");

            var i = 0;

            while (i < number)
            {
                var card = PlayerCardManager.Instance.InstantiateOwnedPlayerCard(title);
                card.Owner = player;
                card.gameObject.transform.rotation = player.Hand.GetHandQueaternion();
                AddCard(card);
                i++;
            }
        }

        public virtual void AddCard(OwnedPlayerCard card)
        {
            collection.Add(card);
            card.gameObject.transform.parent = transform;            
        }

        public virtual void RemoveCard(OwnedPlayerCard card)
        {
            collection.Remove(card);
        }

        public IEnumerable<OwnedPlayerCard> GetCards()
        {
            return collection.AsEnumerable();
        }

        public IEnumerable<PlayerCardType> DestroyRandomCard(int number, string cardType = null)
        {
            List<OwnedPlayerCard> destroyOptions;
            var destroyedTypes = new List<PlayerCardType>();

            if (cardType == null)
                destroyOptions = collection.ToList();
            else
                destroyOptions = collection.Where(c => c.PlayerCard.Type.GetTypeText() == cardType).ToList();

            while (number > 0)
            {
                if (destroyOptions.Count == 0)
                    break;

                var randomCard = destroyOptions[UnityEngine.Random.Range(0, destroyOptions.Count)];
                destroyOptions.Remove(randomCard);
                destroyedTypes.Add(randomCard.PlayerCard.Type);
                randomCard.Destroy();
                number--;
            }

            return destroyedTypes;
        }

        public IEnumerable<PlayerCardType> DestroyAllCards(string cardType = null)
        {
            List<OwnedPlayerCard> destroyOptions;

            if (cardType == null)
                destroyOptions = collection.ToList();
            else
                destroyOptions = collection.Where(c => c.PlayerCard.Type.GetTypeText() == cardType).ToList();

            var destroyedTypes = destroyOptions.Select(card => card.PlayerCard.Type).ToList();

            foreach (var card in destroyOptions)
                card.Destroy();

            return destroyedTypes;
        }
    }
}