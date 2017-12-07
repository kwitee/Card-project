using CardProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardProject.Cards.CardTypes.PlayerCardTypes
{
    [Serializable]
    public class PlayerCardTypeLibrary
    {
        public List<XmlAnything<PlayerCardType>> Library;

        public Dictionary<string, PlayerCardType> ToDictionary()
        {
            return Library.ToDictionary(x => x.Value.Title, x => x.Value);
        }
    }
}