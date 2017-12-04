using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public PlayerCardType Type;
    public const float Width = 8f;
    private static readonly Color noImageColor = new Color(0f, 0f, 0f, 0f);
    private const string spritesPath = "Sprites/";

    [SerializeField]
    private Image cardImage = null;
       
    public void UpdateText()
    {
        foreach (var updateble in GetComponentsInChildren<IUpdatablePlayerCardText>())
            updateble.UpdateText(Type);        
    }

    public void UpdateCardImage()
    {
        var imageSprite = Resources.Load<Sprite>(spritesPath + CardImagePath);

        if (imageSprite != null)
            cardImage.sprite = imageSprite;
        else
            cardImage.color = noImageColor;
    }

    private string CardImagePath
    {
        get { return Type.Title.ToLower().Replace(' ', '_'); }
    }
}