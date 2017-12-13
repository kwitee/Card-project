using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instants
{
    public interface IPlayerCardTypeEnumerable : ICountable
    {
        IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(InstantTriggerArgs args);
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
}