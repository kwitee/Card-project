using UnityEngine;
using UnityEngine.UI;

public class PlayerCardText : MonoBehaviour, IUpdatablePlayerCardText
{
    public void UpdateText(PlayerCardType type)
    {
        GetComponent<Text>().text = type.GetText();
    }
}