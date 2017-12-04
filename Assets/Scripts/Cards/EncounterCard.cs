public class EncounterCard : Card
{
    public EncounterCardType Type;

    public void UpdateText()
    {
        foreach (var updateble in GetComponentsInChildren<IUpdatableEncounterCardText>())
            updateble.UpdateText(Type);
    }

    public void Show()
    {
        ExecuteEffects(Type.CardEffects);
    }

    public void Boon()
    {
        ExecuteEffects(Type.BoonEffects);
    }

    public void Burden()
    {
        ExecuteEffects(Type.BurdenEffects);
    }
}