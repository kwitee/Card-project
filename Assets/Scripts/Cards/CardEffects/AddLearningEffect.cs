public class AddLearningEffect : IQuantifiableInstantCardEffect
{
    public int LearningDelta;

    public void Trigger(Card card)
    {
        card.Owner.AddLearning(LearningDelta);
    }

    public string GetText()
    {
        return string.Format("Learning {0}.", LearningDelta.ToStringWithPlus());
    }

    public void Trigger(Card card, int quantity)
    {
        LearningDelta = quantity;
        Trigger(card);
    }
}