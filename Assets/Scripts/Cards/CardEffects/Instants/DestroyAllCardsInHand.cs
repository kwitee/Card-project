using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instants
{
    public class DestroyAllCardsInHand : IPlayerCardTypeEnumerable
    {
        public string CardType;

        public void Trigger(InstantTriggerArgs args)
        {
            TriggerWithPlayerCardTypes(args);
        }

        public string GetText()
        {
            if (CardType == null)
                return string.Format("Destroy all cards in hand.");
            else
                return string.Format("Destroy all <i>{0}</i> cards in hand.", CardType);
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(InstantTriggerArgs args)
        {
            return args.Player.Hand.DestroyAllCards(CardType);
        }

        public int TriggerWithCount(InstantTriggerArgs args)
        {
            return TriggerWithPlayerCardTypes(args).Count();
        }
    }
}