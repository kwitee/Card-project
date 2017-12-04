using System.Collections.Generic;
using System.Linq;

public class PlayerCardTypeEnumerableCountableConversionInstantEffect : ICountableInstantCardEffect
{
    public XmlAnything<IPlayerCardTypeEnumerableInstantCardEffect> EffectToConvert;
    public XmlAnything<IPlayerCardTypeEnumerableCountableConversion> Conversion;

    public string GetText()
    {
        return EffectToConvert.Value.GetText();
    }

    public void Trigger(Card card)
    {
        EffectToConvert.Value.Trigger(card);
    }

    public int TriggerWithCount(Card card)
    {
        return Conversion.Value.Convert(EffectToConvert.Value.TriggerWithPlayerCardTypes(card));
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