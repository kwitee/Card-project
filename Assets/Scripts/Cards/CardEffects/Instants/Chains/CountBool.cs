using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instants.Chains
{
    public class CountBool : IInstant
    {
        public string Text;
        public XmlAnything<ICountable> FirstEffect;
        public XmlAnything<ICountableModifier> Modifier;
        public XmlAnything<ICountableCondition> Condition;
        public XmlAnything<IInstant> SecondEffect;

        public void Trigger(InstantTriggerArgs args)
        {
            var count = FirstEffect.Value.TriggerWithCount(args);

            if (Modifier != null)
                count = Modifier.Value.ModifyCount(count);

            if (Condition.Value.EvaluateCondition(count))
                SecondEffect.Value.Trigger(args);
        }

        public string GetText()
        {
            return Text;
        }
    }
}