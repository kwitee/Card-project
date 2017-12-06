using CardProject.Cards.CardTypes.PlayerCardTypes;

namespace CardProject.Cards.CardTexts.PlayerCardTexts
{
    public interface IUpdatablePlayerCardText
    {
        void UpdateText(PlayerCardType type);
    }
}