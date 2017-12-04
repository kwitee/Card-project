public class CountBoolChainEffect : IInstantCardEffect
{
    public string Text;
    public XmlAnything<ICountableInstantCardEffect> FirstEffect;
    public XmlAnything<ICountableModifier> Modifier;
    public XmlAnything<ICountableCondition> Condition;
    public XmlAnything<IInstantCardEffect> SecondEffect;

    public void Trigger(OwnedCard card)
    {
        var count = FirstEffect.Value.TriggerWithCount(card);

        if (Modifier != null)
            count = Modifier.Value.ModifyCount(count);

        if (Condition.Value.EvaluateCondition(count))
            SecondEffect.Value.Trigger(card);
    }

    public string GetText()
    {
        return Text;
    }
}