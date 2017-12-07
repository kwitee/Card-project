using CardProject.Cards.CardEffects;
using CardProject.Helpers;
using System.Collections.Generic;
using System.Text;

namespace CardProject.Cards.CardTypes.EncounterCardTypes
{
    public abstract class EncounterCardType
    {
        public string Title;
        public List<XmlAnything<ICardEffect>> CardEffects;
        public List<XmlAnything<ICardEffect>> BoonEffects;
        public List<XmlAnything<ICardEffect>> BurdenEffects;

        public string GetText()
        {
            return GetTextFromEffects(CardEffects);
        }

        public string GetBoonText()
        {
            return GetTextFromEffects(BoonEffects);
        }

        public string GetBurdenText()
        {
            return GetTextFromEffects(BurdenEffects);
        }

        private string GetTextFromEffects(List<XmlAnything<ICardEffect>> effects)
        {
            var strBuilder = new StringBuilder();

            foreach (var effect in effects)
                strBuilder.AppendLine(effect.Value.GetText());

            return strBuilder.ToString();
        }
    }
}