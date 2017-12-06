using CardProject.Cards.CardTypes.EncounterCardTypes;

namespace CardProject.Cards.CardTexts.EncounterCardTexts
{
    public interface IUpdatableEncounterCardText
    {
        void UpdateText(EncounterCardType type);
    }
}