using UnityEngine;

namespace StoneOfAdventure.Core
{
    public class Chest : MonoBehaviour
    {
        private Treasury treasury;
        [SerializeField] private int price = 10;
        private GameObject chestUI;
        private Animator anim;
        private StoneOfAdventure.UI.ArtifactSelector artifactSelector;

        private void Start()
        {
            anim = GetComponent<Animator>();
            treasury = FindObjectOfType<Treasury>();
            chestUI = GetComponentInChildren<Canvas>().gameObject;
            artifactSelector = FindObjectOfType<StoneOfAdventure.UI.ArtifactSelector>();
        }

        // Animation event
        public void TryOpenChest()
        {
            if (price > treasury.CurrentSoulsPoints) return;
            anim.SetTrigger("use");
        }

        public void GiveReward()
        {
            artifactSelector.EnableArtifactSelector();
        }
    }
}
