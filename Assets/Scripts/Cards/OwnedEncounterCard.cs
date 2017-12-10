using UnityEngine;

namespace CardProject.Cards
{
    [RequireComponent(typeof(EncounterCard))]
    public class OwnedEncounterCard : OwnedCard
    {
        public EncounterCard EncounterCard { get; private set; }

        public void Awake()
        {
            EncounterCard = GetComponent<EncounterCard>();
        }

        public void Show()
        {
            ExecuteEffects(EncounterCard.Type.CardEffects);
        }

        public void Boon()
        {
            ExecuteEffects(EncounterCard.Type.BoonEffects);
        }

        public void Burden()
        {
            ExecuteEffects(EncounterCard.Type.BurdenEffects);
        }

        public override void Destroy()
        {
            base.Destroy();
            Destroy(gameObject);
        }    
    }
}