using CardProject.Cards;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.PlayerData
{
    public class PlayerDeck : OwnedPlayerCardCollection
    {
        public IEnumerable<OwnedPlayerCard> Draw(int number, string cardType = null)
        {
            List<OwnedPlayerCard> drawingOptions;

            if (cardType == null)
                drawingOptions = collection.ToList();
            else
                drawingOptions = collection.Where(c => c.PlayerCard.Type.GetTypeText() == cardType).ToList();

            while (number > 0)
            {
                if (drawingOptions.Count == 0)
                    break;

                var drawnCard = drawingOptions[UnityEngine.Random.Range(0, drawingOptions.Count)];
                drawingOptions.Remove(drawnCard);
                collection.Remove(drawnCard);
                number--;
                yield return drawnCard;
            }
        }

        public override void AddCard(OwnedPlayerCard card)
        {
            base.AddCard(card);
            AnimationQueue.Instance.AddAnimation(new Animation(card.gameObject, gameObject.transform.position, false, true, false, false));
            card.State = OwnedPlayerCardState.InDeck;
        }
    }
}