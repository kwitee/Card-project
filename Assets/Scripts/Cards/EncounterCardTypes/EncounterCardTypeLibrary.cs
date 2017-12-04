using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class EncounterCardTypeLibrary
{
    public List<XmlAnything<EncounterCardType>> Library;

    public Dictionary<string, EncounterCardType> ToDictionary()
    {
        return Library.ToDictionary(x => x.Value.Title, x => x.Value);
    }
}