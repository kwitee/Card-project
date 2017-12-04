using UnityEngine;
using UnityEngine.UI;

public class EncounterCardText : MonoBehaviour, IUpdatableEncounterCardText
{
    public void UpdateText(EncounterCardType type)
    {
        GetComponent<Text>().text = type.GetText();
    }
}