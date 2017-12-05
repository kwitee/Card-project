namespace CardProject.CardEffects.Instant
{
    public interface ICountable : IInstant
    {
        int TriggerWithCount(OwnedCard card);
    }

    public interface ICountableModifier
    {
        int ModifyCount(int count);
    }

    public class DivisionCountableModifier : ICountableModifier
    {
        public int Divider;

        public int ModifyCount(int count)
        {
            return count / Divider;
        }
    }

    public interface ICountableCondition
    {
        bool EvaluateCondition(int conditionInput);
    }

    public class HigherThanCountableCondition : ICountableCondition
    {
        public int Bound;

        public bool EvaluateCondition(int conditionInput)
        {
            return conditionInput > Bound;
        }
    }

    public class LessThanCountableCondition : ICountableCondition
    {
        public int Bound;

        public bool EvaluateCondition(int conditionInput)
        {
            return conditionInput < Bound;
        }
    }
}