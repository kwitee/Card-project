using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instant.Chain
{
    public class PlayerCardBool : IInstant
    {
        public string Text;
        public XmlAnything<IPlayerCardTypeEnumerable> FirstEffect;
        public XmlAnything<IPlayerCardTypeEnumerableCondition> Condition;
        public XmlAnything<IInstant> SecondEffect;

        public void Trigger(TriggerArgs args)
        {
            if (Condition.Value.EvaluateCondition(FirstEffect.Value.TriggerWithPlayerCardTypes(args)))
                SecondEffect.Value.Trigger(args);
        }

        public string GetText()
        {
            return Text;
        }
    }
}