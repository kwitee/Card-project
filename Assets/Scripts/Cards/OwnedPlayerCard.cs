using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerCard))]
[RequireComponent(typeof(MovePlayerCard))]
public class OwnedPlayerCard : OwnedCard
{
    public OwnedPlayerCardState State { get; set; }
    public PlayerCard PlayerCard { get; private set; }
    public MovePlayerCard MovePlayerCard { get; private set; }
    public bool Selected { get; private set; }

    public void Awake()
    {
        PlayerCard = GetComponent<PlayerCard>();
        MovePlayerCard = GetComponent<MovePlayerCard>();
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
                Owner.Hand.RemoveCard(this);
                PlayerCard.Type.BeforePlay(Owner);
                ExecuteEffects(PlayerCard.Type.OnPlayCardEffects);

                if (Owner.IsCardCombo(PlayerCard.Type.Title))
                    ExecuteEffects(PlayerCard.Type.OnComboCardEffects);

                Owner.AddCardPlayed(PlayerCard.Type.Title);
                Discard(false);
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

    public void Discard(bool executeDiscardEffect = true)
    {
        Owner.Hand.RemoveCard(this);

        if (!destroyed)
        {
            if (executeDiscardEffect)
                ExecuteEffects(PlayerCard.Type.OnDiscardCardEffects);

            if (!destroyed)
            {
                Selected = false;
                Owner.Deck.AddCard(this);
                State = OwnedPlayerCardState.InDeck;
            }
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
    }
}

public enum OwnedPlayerCardState
{
    InHand,
    InDeck
}