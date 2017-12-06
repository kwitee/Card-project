namespace CardProject.Cards.CardTypes.PlayerCardTypes
{
    public class ActionConditionCardType : ActionCardType
    {
        public override string GetTypeText()
        {
            return base.GetTypeText() + " - Condition";
        }
    }
}