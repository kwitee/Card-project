namespace CardProject.Cards.CardEffects.Instant.Chain
{
    public class Count : IInstant
    {
        public string Text;
        public XmlAnything<ICountable> FirstEffect;
        public XmlAnything<ICountableModifier> Modifier;
        public XmlAnything<IQuantifiable> SecondEffect;

        public void Trigger(OwnedCard card)
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
}