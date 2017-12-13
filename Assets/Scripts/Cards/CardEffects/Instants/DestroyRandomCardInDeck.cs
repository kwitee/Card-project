using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instants
{
    public class DestroyRandomCardInDeck : IPlayerCardTypeEnumerable
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
                    return string.Format("Destroy random card in deck.");
                else
                    return string.Format("Destroy random <i>{0}</i> card in deck.", CardType);
            }
            else
            {
                if (CardType == null)
                    return string.Format("Destroy {0} random cards in deck.", NumberOfCards);
                else
                    return string.Format("Destroy {0} <i>{1}</i> random cards in deck.", NumberOfCards, CardType);
            }
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(InstantTriggerArgs args)
        {
            return args.Player.Deck.DestroyRandomCard(NumberOfCards, CardType);
        }

        public int TriggerWithCount(InstantTriggerArgs args)
        {
            return TriggerWithPlayerCardTypes(args).Count();
        }
    }
}