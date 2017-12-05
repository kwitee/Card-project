public class ResetStarvationEffect : IInstantCardEffect
{
    public void Trigger(OwnedCard card)
    {
        card.Owner.ResetStarvation();
    }

    public string GetText()
    {
        return string.Format("Starvation reset.");
    }
}