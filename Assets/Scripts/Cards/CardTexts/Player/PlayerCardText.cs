using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards.CardTexts.Player
{
    public class PlayerCardText : MonoBehaviour, IUpdatablePlayerCardText
    {
        public void UpdateText(PlayerCardType type)
        {
            GetComponent<Text>().text = type.GetText();
        }
    }
}