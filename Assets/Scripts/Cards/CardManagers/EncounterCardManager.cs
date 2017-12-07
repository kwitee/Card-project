using CardProject.Cards.CardManagers.EncounterDeck;
using CardProject.Cards.CardTypes.EncounterCardTypes;
using CardProject.GameLogic;
using CardProject.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace CardProject.Cards.CardManagers
{
    public sealed class EncounterCardManager : Singleton<EncounterCardManager>
    {
        [SerializeField]
        private GameObject worldCardPrefab = null;

        [SerializeField]
        private GameObject enemyCardPrefab = null;

        private const string xmlPath = @"CardData/encounter_cards";
        private const string tierOnePath = @"CardData/encounter_deck_tier_one";
        private const string tierTwoPath = @"CardData/encounter_deck_tier_two";
        private const string tierThreePath = @"CardData/encounter_deck_tier_three";

        private Dictionary<string, EncounterCardType> cardLibrary;
        private List<EncounterCardType> deck;
        private EncounterDeckPrescription tierOneDeckPrescription = null;
        private EncounterDeckPrescription tierTwoDeckPrescription = null;
        private EncounterDeckPrescription tierThreeDeckPrescription = null;

        public OwnedEncounterCard InstantiateRandomCard()
        {
            var randomCard = deck[UnityEngine.Random.Range(0, deck.Count)];
            return InstantiateCard(randomCard);
        }

        private OwnedEncounterCard InstantiateCard(EncounterCardType type)
        {
            GameObject prefab = SelectPrefab(type);
            var instance = Instantiate(prefab);
            var encounterCard = instance.GetComponent<EncounterCard>();
            encounterCard.Type = type;
            encounterCard.UpdateText();
            return instance.AddComponent<OwnedEncounterCard>();
        }

        private GameObject SelectPrefab(EncounterCardType type)
        {
            if (type is WorldCardType)
                return worldCardPrefab;
            else if (type is EnemyCardType)
                return enemyCardPrefab;
            else
                throw new Exception("There is no prefab for this card type!");
        }

        public void Awake()
        {
            ParseLibrary();
            ParseDeckPrescription(ref tierOneDeckPrescription, tierOnePath);
            ParseDeckPrescription(ref tierTwoDeckPrescription, tierTwoPath);
            ParseDeckPrescription(ref tierThreeDeckPrescription, tierThreePath);
        }

        private void ParseLibrary()
        {
            EncounterCardTypeLibrary library;
            var xmlData = Resources.Load<TextAsset>(xmlPath);

            using (var reader = new StringReader(xmlData.text))
            {
                var deserializer = new XmlSerializer(typeof(EncounterCardTypeLibrary));
                library = deserializer.Deserialize(reader) as EncounterCardTypeLibrary;
            }

            cardLibrary = library.ToDictionary();
        }

        private void ParseDeckPrescription(ref EncounterDeckPrescription deckPrescription, string filePath)
        {
            var jsonData = Resources.Load<TextAsset>(filePath);

            using (var r = new StringReader(jsonData.text))
            {
                string json = r.ReadToEnd();
                deckPrescription = JsonUtility.FromJson<EncounterDeckPrescription>(json);
            }
        }

        public void UpdateEncounterDeck()
        {
            var deckPrescription = ChooseDeckPrescription();

            if (deck == null)
                deck = new List<EncounterCardType>();
            else
                deck.Clear();

            foreach (var record in deckPrescription.Records)
            {
                int quantity = record.Quantity;

                while (quantity-- > 0)
                    deck.Add(cardLibrary[record.Title]);
            }
        }

        private EncounterDeckPrescription ChooseDeckPrescription()
        {
            switch (GameManager.Instance.Tier)
            {
                case 1:
                    return tierOneDeckPrescription;
                case 2:
                    return tierTwoDeckPrescription;
                default:
                    return tierThreeDeckPrescription;
            }
        }
    }
}