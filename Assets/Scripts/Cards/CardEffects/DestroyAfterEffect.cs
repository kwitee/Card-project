public class DestroyAfterEffect : IInstantCardEffect
{
    public void Trigger(Card card)
    {
        card.Destroy();
    }

    public string GetText()
    {
        return string.Format("Destroy this card.");
    }
}