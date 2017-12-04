using UnityEngine;
using UnityEngine.UI;

public class PlayerCardTypeText : MonoBehaviour, IUpdatablePlayerCardText
{
    public void UpdateText(PlayerCardType type)
    {
        GetComponent<Text>().text = type.GetTypeText();
    }
}