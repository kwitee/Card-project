using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instants
{
    public class DiscardRandomCard : IPlayerCardTypeEnumerable
    {
        public int NumberOfCards;
        public string CardType;

        public void Trigger(InstantTriggerArgs args)
        {
            TriggerWithPlayerCardTypes(args);
        }

        public string GetText()
        {
            if (NumberOfCards == 1)
            {
                if (CardType == null)
                    return string.Format("Discard random card in hand.");
                else
                    return string.Format("Discard random <i>{0}</i> card in hand.", CardType);
            }
            else
            {
                if (CardType == null)
                    return string.Format("Discard {0} random cards in hand.", NumberOfCards);
                else
                    return string.Format("Discard {0} <i>{1}</i> random cards in hand.", NumberOfCards, CardType);
            }
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(InstantTriggerArgs args)
        {
            return args.Player.Hand.DiscardRandomCard(NumberOfCards, CardType);
        }

        public int TriggerWithCount(InstantTriggerArgs args)
        {
            return TriggerWithPlayerCardTypes(args).Count();
        }
    }
}