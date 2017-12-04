public class PlayerCardBoolChainEffect : IInstantCardEffect
{
    public string Text;
    public XmlAnything<IPlayerCardTypeEnumerableInstantCardEffect> FirstEffect;
    public XmlAnything<IPlayerCardTypeEnumerableCondition> Condition;
    public XmlAnything<IInstantCardEffect> SecondEffect;

    public void Trigger(OwnedCard card)
    {
        if (Condition.Value.EvaluateCondition(FirstEffect.Value.TriggerWithPlayerCardTypes(card)))
            SecondEffect.Value.Trigger(card);
    }

    public string GetText()
    {
        return Text;
    }
}