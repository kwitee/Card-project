using UnityEngine;
using UnityEngine.UI;

public class LearningPlayerCardText : MonoBehaviour, IUpdatablePlayerCardText
{
    public void UpdateText(PlayerCardType type)
    {
        GetComponent<Text>().text = type.LearningCost.ToString();
    }
}