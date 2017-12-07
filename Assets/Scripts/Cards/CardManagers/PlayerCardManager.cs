using CardProject.Cards.CardTypes.PlayerCardTypes;
using CardProject.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace CardProject.Cards.CardManagers
{
    public sealed class PlayerCardManager : Singleton<PlayerCardManager>
    {
        [SerializeField]
        private GameObject combatCardPrefab = null;

        [SerializeField]
        private GameObject actionCardPrefab = null;

        [SerializeField]
        private GameObject actionFoodCardPrefab = null;

        [SerializeField]
        private GameObject actionLearningCardPrefab = null;

        [SerializeField]
        private GameObject actionConditionCardPrefab = null;

        private const string xmlPath = @"CardData/player_cards";
        private Dictionary<string, PlayerCardType> cardLibrary;

        public OwnedPlayerCard InstantiateOwnedPlayerCard(string title)
        {
            return InstantiateOwnedPlayerCard(cardLibrary[title]);
        }

        public OwnedPlayerCard InstantiateOwnedPlayerCard(PlayerCardType type)
        {
            var prefab = SelectPrefab(type);
            var instance = Instantiate(prefab);
            var playerCard = instance.GetComponent<PlayerCard>();
            playerCard.Type = type;
            playerCard.UpdateText();
            playerCard.UpdateCardImage();
            return instance.AddComponent<OwnedPlayerCard>();
        }

        public LearningPoolPlayerCard InstantiateLearningPoolCard(PlayerCardType type)
        {
            var prefab = SelectPrefab(type);
            var instance = Instantiate(prefab);
            var playerCard = instance.GetComponent<PlayerCard>();
            playerCard.Type = type;
            playerCard.UpdateText();
            playerCard.UpdateCardImage();
            return instance.AddComponent<LearningPoolPlayerCard>();
        }

        public IEnumerable<LearningPoolPlayerCard> InstantiateLearningPoolCards(CardSet set)
        {
            foreach (var type in cardLibrary.Where(x => x.Value.Set == set && x.Value.Learnable).Select(x => x.Value))
                yield return InstantiateLearningPoolCard(type);
        }

        private GameObject SelectPrefab(PlayerCardType type)
        {
            if (type is CombatCardType)
                return combatCardPrefab;
            else if (type is ActionFoodCardType)
                return actionFoodCardPrefab;
            else if (type is ActionLearningCardType)
                return actionLearningCardPrefab;
            else if (type is ActionConditionCardType)
                return actionConditionCardPrefab;
            else if (type is ActionCardType)
                return actionCardPrefab;
            else
                throw new Exception("There is no prefab for this card type!");
        }

        public void Awake()
        {
            PlayerCardTypeLibrary library;
            var xmlData = Resources.Load<TextAsset>(xmlPath);

            using (var reader = new StringReader(xmlData.text))
            {
                var deserializer = new XmlSerializer(typeof(PlayerCardTypeLibrary));
                library = deserializer.Deserialize(reader) as PlayerCardTypeLibrary;
            }

            cardLibrary = library.ToDictionary();
        }
    }
}