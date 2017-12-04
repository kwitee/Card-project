public class CountChainEffect : IInstantCardEffect
{
    public string Text;
    public XmlAnything<ICountableInstantCardEffect> FirstEffect;
    public XmlAnything<ICountableModifier> Modifier;
    public XmlAnything<IQuantifiableInstantCardEffect> SecondEffect;    

    public void Trigger(Card card)
    {
        var count = FirstEffect.Value.TriggerWithCount(card);

        if (Modifier != null)
            count = Modifier.Value.ModifyCount(count);

        SecondEffect.Value.Trigger(card, count);
    }

    public string GetText()
    {
        return Text;
    }
}