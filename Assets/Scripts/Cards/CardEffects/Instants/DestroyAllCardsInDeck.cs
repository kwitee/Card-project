using CardProject.Cards.CardTypes.PlayerCardTypes;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardEffects.Instants
{
    public class DestroyAllCardsInDeck : IPlayerCardTypeEnumerable
    {
        public string CardType;

        public void Trigger(InstantTriggerArgs args)
        {
            TriggerWithPlayerCardTypes(args);
        }

        public string GetText()
        {
            if (CardType == null)
                return string.Format("Destroy all cards in deck.");
            else
                return string.Format("Destroy all <i>{0}</i> cards in deck.", CardType);
        }

        public IEnumerable<PlayerCardType> TriggerWithPlayerCardTypes(InstantTriggerArgs args)
        {
            return args.Player.Deck.DestroyAllCards(CardType);
        }

        public int TriggerWithCount(InstantTriggerArgs args)
        {
            return TriggerWithPlayerCardTypes(args).Count();
        }
    }
}