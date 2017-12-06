using UnityEngine;
using UnityEngine.UI;

namespace CardProject.Cards
{
    public abstract class Card : MonoBehaviour
    {
        protected static readonly Color noImageColor = new Color(0f, 0f, 0f, 0f);
        protected const string spritesPath = "Sprites/";

        [SerializeField]
        protected Image cardImage = null;

        public abstract void UpdateText();

        public void UpdateCardImage()
        {
            if (cardImage != null)
            {
                var imageSprite = Resources.Load<Sprite>(spritesPath + CardImagePath);

                if (imageSprite != null)
                    cardImage.sprite = imageSprite;
                else
                    cardImage.color = noImageColor;
            }
        }

        protected abstract string CardImagePath { get; }
    }
}