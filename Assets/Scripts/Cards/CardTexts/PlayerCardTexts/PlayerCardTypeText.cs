using CardProject.Cards.CardTypes.PlayerCardTypes;
using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards.CardTexts.PlayerCardTexts
{
    public class PlayerCardTypeText : MonoBehaviour, IUpdatablePlayerCardText
    {
        public void UpdateText(PlayerCardType type)
        {
            GetComponent<Text>().text = type.GetTypeText();
        }
    }
}