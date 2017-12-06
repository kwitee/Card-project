using CardProject.Cards.CardTexts.EncounterCardTexts;
using CardProject.Cards.CardTypes.EncounterCardTypes;

namespace CardProject.Cards
{
    public class EncounterCard : Card
    {
        public EncounterCardType Type;

        protected override string CardImagePath
        {
            get { return Type.Title.ToLower().Replace(' ', '_'); }
        }

        public override void UpdateText()
        {
            foreach (var updateble in GetComponentsInChildren<IUpdatableEncounterCardText>())
                updateble.UpdateText(Type);
        }
    }
}