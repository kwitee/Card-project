using UnityEngine;
using UnityEngine.UI;

public class BurdenEncounterCardText : MonoBehaviour, IUpdatableEncounterCardText
{
    public void UpdateText(EncounterCardType type)
    {
        GetComponent<Text>().text = type.GetBurdenText();
    }
}