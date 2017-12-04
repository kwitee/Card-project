using UnityEngine;
using UnityEngine.UI;

public class BoonEncounterCardText : MonoBehaviour, IUpdatableEncounterCardText
{
    public void UpdateText(EncounterCardType type)
    {
        GetComponent<Text>().text = type.GetBoonText();
    }
}