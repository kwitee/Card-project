namespace CardProject.Cards.CardEffects.Auras
{
    public interface IAuraTriggerCondition
    {
        bool EvaluateCondition(AuraTriggerArgs args);
    }

    public class PlayerCardTypeAuraCondition : IAuraTriggerCondition
    {
        public string CardType;

        public bool EvaluateCondition(AuraTriggerArgs args)
        {
            return args.Card.PlayerCard.Type.GetTypeText() == CardType;
        }
    }
}