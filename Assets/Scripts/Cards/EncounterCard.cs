using CardProject.Cards.CardTexts.Encounter;

public class EncounterCard : Card
{
    public EncounterCardType Type;

    protected override string CardImagePath
    {
        get { return Type.Title.ToLower().Replace(' ', '_'); }
    }

    public override void UpdateText()
    {
        foreach (var updateble in GetComponentsInChildren<IUpdatableEncounterCardText>())
            updateble.UpdateText(Type);
    }
}