using CardProject.Cards.CardTypes.PlayerCardTypes;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards.CardTexts.PlayerCardTexts
{
    public class ActionPlayerCardText : MonoBehaviour, IUpdatablePlayerCardText
    {
        public void UpdateText(PlayerCardType type)
        {
            if (type is ActionCardType)
                GetComponent<Text>().text = (type as ActionCardType).ActionCost.ToString();
            else
                throw new Exception("ActionPlayerCardText is not compatible with non-action card type!");
        }
    }
}