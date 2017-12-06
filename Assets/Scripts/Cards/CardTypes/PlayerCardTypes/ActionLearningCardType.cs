namespace CardProject.Cards.CardTypes.PlayerCardTypes
{
    public class ActionLearningCardType : ActionCardType
    {
        public override string GetTypeText()
        {
            return base.GetTypeText() + " - Learning";
        }
    }
}