using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instant.Chain
{
    public class Count : IInstant
    {
        public string Text;
        public XmlAnything<ICountable> FirstEffect;
        public XmlAnything<ICountableModifier> Modifier;
        public XmlAnything<IQuantifiable> SecondEffect;

        public void Trigger(TriggerArgs args)
        {
            var count = FirstEffect.Value.TriggerWithCount(args);

            if (Modifier != null)
                count = Modifier.Value.ModifyCount(count);

            SecondEffect.Value.Trigger(args, count);
        }

        public string GetText()
        {
            return Text;
        }
    }
}