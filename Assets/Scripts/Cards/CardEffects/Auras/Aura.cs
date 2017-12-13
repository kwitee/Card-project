using CardProject.Helpers;
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
        public XmlAnything<IAuraTriggerCondition> Condition;

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

        public abstract void OnTrigger(AuraTriggerArgs args);

        public void Trigger(AuraTriggerArgs args)
        {
            if (Condition == null || Condition.Value.EvaluateCondition(args))
            {
                numberOfTrigers++;

                if (AllowedNumberOfTriggers > 0 && numberOfTrigers >= AllowedNumberOfTriggers)
                    Unregister();

                OnTrigger(args);
            }
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

    public class AuraTriggerArgs
    {
        public OwnedPlayerCard Card { get; private set; }

        public AuraTriggerArgs(OwnedPlayerCard card)
        {
            Card = card;
        }
    }
}