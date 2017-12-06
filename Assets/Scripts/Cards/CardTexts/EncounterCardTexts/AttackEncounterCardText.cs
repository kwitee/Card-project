using CardProject.Cards.CardTypes.EncounterCardTypes;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards.CardTexts.EncounterCardTexts
{
    public class AttackEncounterCardText : MonoBehaviour, IUpdatableEncounterCardText
    {
        public void UpdateText(EncounterCardType type)
        {
            if (type is WorldCardType)
                GetComponent<Text>().text = (type as WorldCardType).Attack.ToString();
            else
                throw new Exception("AttackEncounterCardText is not compatible with non-world card type!");
        }
    }
}