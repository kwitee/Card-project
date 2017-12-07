using CardProject.Cards.CardEffects;
using CardProject.Helpers;
using CardProject.PlayerData;
using System.Collections.Generic;
using System.Text;

namespace CardProject.Cards.CardTypes.PlayerCardTypes
{
    public abstract class PlayerCardType
    {
        public string Title;
        public string Text;
        public int LearningCost;
        public CardSet Set;
        public bool Learnable;
        public List<XmlAnything<ICardEffect>> OnPlayCardEffects;
        public List<XmlAnything<ICardEffect>> OnDrawCardEffects;
        public List<XmlAnything<ICardEffect>> OnDiscardCardEffects;
        public List<XmlAnything<ICardEffect>> OnComboCardEffects;

        public string GetText()
        {
            if (Text != null)
                return Text;

            var strBuilder = new StringBuilder();

            foreach (var effect in OnPlayCardEffects)
                strBuilder.AppendLine(effect.Value.GetText());

            if (OnComboCardEffects.Count > 0)
            {
                strBuilder.AppendLine("COMBO:");

                foreach (var effect in OnComboCardEffects)
                    strBuilder.AppendLine(effect.Value.GetText());
            }

            if (OnDiscardCardEffects.Count > 0)
            {
                strBuilder.AppendLine("DISCARD:");

                foreach (var effect in OnDiscardCardEffects)
                    strBuilder.AppendLine(effect.Value.GetText());
            }

            if (OnDrawCardEffects.Count > 0)
            {
                strBuilder.AppendLine("DRAW:");

                foreach (var effect in OnDrawCardEffects)
                    strBuilder.AppendLine(effect.Value.GetText());
            }

            return strBuilder.ToString();
        }

        public virtual bool IsCardPlayable(Player owner)
        {
            return true;
        }

        public virtual void BeforePlay(Player owner)
        {
        }

        public abstract string GetTypeText();
    }
}