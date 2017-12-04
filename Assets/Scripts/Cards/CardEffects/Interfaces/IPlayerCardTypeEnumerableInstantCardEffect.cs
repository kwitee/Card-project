using System.Collections.Generic;
using System.Linq;

public interface IPlayerCardTypeEnumerableInstantCardEffect : ICountableInstantCardEffect
{
    IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(Card card);
}

public interface IPlayerCardTypeEnumerableCondition
{
    bool EvaluateCondition(IEnumerable<PlayerCardType> conditionInput);
}

public class PlayerCardTypeEnumerableCondition : IPlayerCardTypeEnumerableCondition
{
    public string CardType;

    public bool EvaluateCondition(IEnumerable<PlayerCardType> conditionInput)
    {
        return conditionInput.Any(c => c.GetTypeText() == CardType);
    }
}

public class NotPlayerCardTypeEnumerableCondition : IPlayerCardTypeEnumerableCondition
{
    public string CardType;

    public bool EvaluateCondition(IEnumerable<PlayerCardType> conditionInput)
    {
        return !conditionInput.Any(c => c.GetTypeText() == CardType);
    }
}