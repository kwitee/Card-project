namespace CardProject.Cards.CardTypes.PlayerCardTypes
{
    public class ActionFoodCardType : ActionCardType
    {
        public override string GetTypeText()
        {
            return base.GetTypeText() + " - Food";
        }
    }
}