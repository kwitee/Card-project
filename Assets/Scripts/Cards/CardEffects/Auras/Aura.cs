using CardProject.PlayerData;
using System.Xml.Serialization;

namespace CardProject.Cards.CardEffects.Auras
{
    public abstract class Aura : ICardEffect
    {
        public string Text;
        public int AllowedNumberOfTriggers;
        public AutoUnregister AutoUnregister;
        public TriggerEvent TriggerEvent;

        [XmlIgnore]
        public Player Player { get; private set; }

        [XmlIgnore]
        protected int numberOfTrigers;

        public void Register(Player player)
        {
            Player = player;
            AuraCollection.Instance.Register(this);
        }

        public void Unregister()
        {
            AuraCollection.Instance.Unregister(this);
        }

        public virtual void Trigger(OwnedPlayerCard card)
        {
            numberOfTrigers++;

            if (AllowedNumberOfTriggers > 0 && numberOfTrigers >= AllowedNumberOfTriggers)
                Unregister();
        }

        public string GetText()
        {
            return Text;
        }
    }

    public enum AutoUnregister
    {
        None,
        PhaseEnd,
        TurnEnd
    }

    public enum TriggerEvent
    {
        CardPlayed,
        CardDiscarded,
        CardDrown
    }
}