using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instants
{
    public class DiscardAllCards : IPlayerCardTypeEnumerable
    {
        public string CardType;

        public void Trigger(InstantTriggerArgs args)
        {
            TriggerWithPlayerCardTypes(args);
        }

        public string GetText()
        {
            if (CardType == null)
                return string.Format("Discard all cards.");
            else
                return string.Format("Discard all {0} cards.", CardType);
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(InstantTriggerArgs args)
        {
            return args.Player.Hand.DiscardAllCards(CardType);
        }

        public int TriggerWithCount(InstantTriggerArgs args)
        {
            return TriggerWithPlayerCardTypes(args).Count();
        }
    }
}