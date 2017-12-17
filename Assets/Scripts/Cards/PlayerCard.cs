using CardProject.Cards.CardTexts.PlayerCardTexts;
using CardProject.Cards.CardTypes.PlayerCardTypes;
using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards
{
    public class PlayerCard : Card
    {
        [SerializeField]
        protected Image highlightLearnImage = null;

        [SerializeField]
        protected Image highlightPlayImage = null;

        public PlayerCardType Type;
        public const float Width = 8.5f;

        protected override string CardImagePath
        {
            get { return Type.Title.ToLower().Replace(' ', '_'); }
        }

        public override void UpdateText()
        {
            foreach (var updateble in GetComponentsInChildren<IUpdatablePlayerCardText>())
                updateble.UpdateText(Type);
        }

        public void ChangeHighlightPlayable(bool value)
        {
            highlightPlayImage.gameObject.SetActive(value);
        }

        public void ChangeHighlightLearnable(bool value)
        {
            highlightLearnImage.gameObject.SetActive(value);
        }
    }
}