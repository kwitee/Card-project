public class ActionFoodCardType : ActionCardType
{
    public override string GetTypeText()
    {
        return base.GetTypeText() + " - Food";
    }
}