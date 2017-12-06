using CardProject.Cards.CardTexts.Player;

public class PlayerCard : Card
{
    public PlayerCardType Type;
    public const float Width = 8f;

    protected override string CardImagePath
    {
        get { return Type.Title.ToLower().Replace(' ', '_'); }
    }

    public override void UpdateText()
    {
        foreach (var updateble in GetComponentsInChildren<IUpdatablePlayerCardText>())
            updateble.UpdateText(Type);
    }
}