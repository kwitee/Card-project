using CardProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardTypes.EncounterCardTypes
{
    [Serializable]
    public class EncounterCardTypeLibrary
    {
        public List<XmlAnything<EncounterCardType>> Library;

        public Dictionary<string, EncounterCardType> ToDictionary()
        {
            return Library.ToDictionary(x => x.Value.Title, x => x.Value);
        }
    }
}