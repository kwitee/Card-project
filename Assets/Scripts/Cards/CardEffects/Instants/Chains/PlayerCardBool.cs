using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instants.Chains
{
    public class PlayerCardBool : IInstant
    {
        public string Text;
        public XmlAnything<IPlayerCardTypeEnumerable> FirstEffect;
        public XmlAnything<IPlayerCardTypeEnumerableCondition> Condition;
        public XmlAnything<IInstant> SecondEffect;

        public void Trigger(InstantTriggerArgs args)
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