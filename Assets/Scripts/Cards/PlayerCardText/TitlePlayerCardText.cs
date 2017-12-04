using UnityEngine;
using UnityEngine.UI;

public class TitlePlayerCardText : MonoBehaviour, IUpdatablePlayerCardText
{
    public void UpdateText(PlayerCardType type)
    {
        GetComponent<Text>().text = type.Title;
    }
}