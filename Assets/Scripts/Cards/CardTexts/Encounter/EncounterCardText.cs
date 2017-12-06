using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards.CardTexts.Encounter
{
    public class EncounterCardText : MonoBehaviour, IUpdatableEncounterCardText
    {
        public void UpdateText(EncounterCardType type)
        {
            GetComponent<Text>().text = type.GetText();
        }
    }
}