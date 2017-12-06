using CardProject.PlayerData;

namespace CardProject.Cards.CardTypes.PlayerCardTypes
{
    public class ActionCardType : PlayerCardType
    {
        public int ActionCost;

        public override bool IsCardPlayable(Player owner)
        {
            return owner.CanPlayAction(ActionCost);
        }

        public override void BeforePlay(Player owner)
        {
            owner.AddAction(-ActionCost);
        }

        public override string GetTypeText()
        {
            return "Action";
        }
    }
}