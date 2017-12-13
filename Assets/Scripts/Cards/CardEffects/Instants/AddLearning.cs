using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instants
{
    public class AddLearning : IQuantifiable
    {
        public int LearningDelta;

        public void Trigger(InstantTriggerArgs args)
        {
            args.Player.AddLearning(LearningDelta);
        }

        public string GetText()
        {
            return string.Format("Learning {0}.", LearningDelta.ToStringWithPlus());
        }

        public void Trigger(InstantTriggerArgs args, int quantity)
        {
            LearningDelta = quantity;
            Trigger(args);
        }
    }
}