using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards.CardTexts.Player
{
    public class LearningPlayerCardText : MonoBehaviour, IUpdatablePlayerCardText
    {
        public void UpdateText(PlayerCardType type)
        {
            GetComponent<Text>().text = type.LearningCost.ToString();
        }
    }
}