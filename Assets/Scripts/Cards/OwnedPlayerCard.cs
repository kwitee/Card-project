using CardProject.GameLogic;
using CardProject.Gui;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardProject.Cards
{
    [RequireComponent(typeof(PlayerCard))]
    public class OwnedPlayerCard : OwnedCard
    {
        public OwnedPlayerCardState State { get; set; }
        public PlayerCard PlayerCard { get; private set; }
        public bool Selected { get; private set; }
        private static readonly Vector3 showPosition = new Vector3(0, 0, -0.3f);

        public void Awake()
        {
            PlayerCard = GetComponent<PlayerCard>();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            var trigger = GetComponent<EventTrigger>();

            var pcEntry = new EventTrigger.Entry();
            pcEntry.eventID = EventTriggerType.PointerClick;
            pcEntry.callback.AddListener((data) => { PointerClick(); });
            trigger.triggers.Add(pcEntry);

            var peEntry = new EventTrigger.Entry();
            peEntry.eventID = EventTriggerType.PointerEnter;
            peEntry.callback.AddListener((data) => { PointerEnter(); });
            trigger.triggers.Add(peEntry);

            var pexEntry = new EventTrigger.Entry();
            pexEntry.eventID = EventTriggerType.PointerExit;
            pexEntry.callback.AddListener((data) => { PointerExit(); });
            trigger.triggers.Add(pexEntry);
        }

        public void Play()
        {
            if (GameManager.Instance.IsCardPlayable(this))
            {
                if (PlayerCard.Type.IsCardPlayable(Owner))
                {
                    AnimationQueue.Instance.AddAnimation(new Animation(gameObject, showPosition, false, false, false, false));
                    Owner.Hand.RemoveCard(this);
                    PlayerCard.Type.BeforePlay(Owner);
                    ExecuteEffects(PlayerCard.Type.OnPlayCardEffects);

                    if (Owner.IsCardCombo(PlayerCard.Type.Title))
                        ExecuteEffects(PlayerCard.Type.OnComboCardEffects);

                    Owner.AddCardPlayed(PlayerCard.Type.Title);
                    MoveToDeck();
                }
                else
                    GuiManager.Instance.ShowFadeOutText("You don't have enough Action!");
            }
            else
                GuiManager.Instance.ShowFadeOutText("Card's not playable in this phase!");
        }

        public void ExecuteDrawEffects()
        {
            ExecuteEffects(PlayerCard.Type.OnDrawCardEffects);
        }

        public void Discard()
        {
            Owner.Hand.RemoveCard(this);

            if (!destroyed)
            {
                ExecuteEffects(PlayerCard.Type.OnDiscardCardEffects);
                MoveToDeck();
            }
        }

        public void MoveToDeck()
        {
            if (!destroyed)
            {
                Selected = false;
                Owner.Deck.AddCard(this);
                State = OwnedPlayerCardState.InDeck;
            }
        }

        public void PointerClick()
        {
            switch (State)
            {
                case OwnedPlayerCardState.InHand:
                    Play();
                    break;
                case OwnedPlayerCardState.InDeck:
                    Owner.ChangeStatisticsType();
                    break;
            }
        }

        public void PointerEnter()
        {
            switch (State)
            {
                case OwnedPlayerCardState.InHand:
                    Selected = true;
                    Owner.Hand.RefreshHand();
                    break;
                case OwnedPlayerCardState.InDeck:
                    Owner.ShowCardStatistics();
                    break;
            }
        }

        public void PointerExit()
        {
            switch (State)
            {
                case OwnedPlayerCardState.InHand:
                    Selected = false;
                    Owner.Hand.RefreshHand();
                    break;
                case OwnedPlayerCardState.InDeck:
                    Owner.HideCardStatistics();
                    break;
            }
        }

        public override void Destroy()
        {
            Owner.Hand.RemoveCard(this);
            Owner.Deck.RemoveCard(this);
            base.Destroy();
            var rotateBefore = (State == OwnedPlayerCardState.InDeck);
            AnimationQueue.Instance.AddAnimation(new Animation(gameObject, showPosition, rotateBefore, false, false, false, true));
        }
    }

    public enum OwnedPlayerCardState
    {
        InHand,
        InDeck
    }
}