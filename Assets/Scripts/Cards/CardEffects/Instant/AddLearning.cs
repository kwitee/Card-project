using CardProject.Helpers;

namespace CardProject.Cards.CardEffects.Instant
{
    public class AddLearning : IQuantifiable
    {
        public int LearningDelta;

        public void Trigger(OwnedCard card)
        {
            card.Owner.AddLearning(LearningDelta);
        }

        public string GetText()
        {
            return string.Format("Learning {0}.", LearningDelta.ToStringWithPlus());
        }

        public void Trigger(OwnedCard card, int quantity)
        {
            LearningDelta = quantity;
            Trigger(card);
        }
    }
}