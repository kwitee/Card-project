using CardProject.Cards.CardTypes.PlayerCardTypes;
using CardProject.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instants.Conversions
{
    public class PlayerCardTypeEnumerableCountable : ICountable
    {
        public XmlAnything<IPlayerCardTypeEnumerable> EffectToConvert;
        public XmlAnything<IPlayerCardTypeEnumerableCountableConversion> Conversion;

        public string GetText()
        {
            return EffectToConvert.Value.GetText();
        }

        public void Trigger(InstantTriggerArgs args)
        {
            EffectToConvert.Value.Trigger(args);
        }

        public int TriggerWithCount(InstantTriggerArgs args)
        {
            return Conversion.Value.Convert(EffectToConvert.Value.TriggerWithPlayerCardTypes(args));
        }
    }

    public interface IPlayerCardTypeEnumerableCountableConversion
    {
        int Convert(IEnumerable<PlayerCardType> cards);
    }

    public class SumLearningCost : IPlayerCardTypeEnumerableCountableConversion
    {
        public int Convert(IEnumerable<PlayerCardType> cards)
        {
            return cards.Sum(card => card.LearningCost);
        }
    }
}