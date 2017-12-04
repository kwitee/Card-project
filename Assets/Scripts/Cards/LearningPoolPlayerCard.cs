using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerCard))]
public class LearningPoolPlayerCard : MonoBehaviour
{
    public PlayerCard PlayerCard { get; private set; }

    public void Awake()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        PlayerCard = GetComponent<PlayerCard>();
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { PointerClick(); });
        GetComponent<EventTrigger>().triggers.Add(entry);
    }

    public void PointerClick()
    {
        LearningPool.Instance.LearnCard(PlayerCard.Type);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Move(Vector3 position)
    {
        transform.position += position;
    }
}