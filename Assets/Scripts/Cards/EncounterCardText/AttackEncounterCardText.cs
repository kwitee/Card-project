using System;
using UnityEngine;
using UnityEngine.UI;

public class AttackEncounterCardText : MonoBehaviour, IUpdatableEncounterCardText
{
    public void UpdateText(EncounterCardType type)
    {
        if (type is WorldCardType)
            GetComponent<Text>().text = (type as WorldCardType).Attack.ToString();
        else
            throw new Exception("AttackEncounterCardText is not compatible with non-world card type!");
    }
}