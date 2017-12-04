using UnityEngine;
using UnityEngine.UI;

public class TitleEncounterCardText : MonoBehaviour, IUpdatableEncounterCardText
{
    public void UpdateText(EncounterCardType type)
    {
        GetComponent<Text>().text = type.Title;
    }
}