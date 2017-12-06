using CardProject.Cards.CardTypes.EncounterCardTypes;
using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards.CardTexts.EncounterCardTexts
{
    public class BurdenEncounterCardText : MonoBehaviour, IUpdatableEncounterCardText
    {
        public void UpdateText(EncounterCardType type)
        {
            GetComponent<Text>().text = type.GetBurdenText();
        }
    }
}