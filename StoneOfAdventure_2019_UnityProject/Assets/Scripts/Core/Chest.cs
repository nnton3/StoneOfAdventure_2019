using UnityEngine;
using StoneOfAdventure.UI;
namespace StoneOfAdventure.Core
{
    public class Chest : MonoBehaviour
    {
        private Treasury treasury;
        [SerializeField] private int price = 10;
        private GameObject chestUI;
        private Animator anim;
        private ArtifactSelector artifactSelector;

        private void Start()
        {
            anim = GetComponent<Animator>();
            treasury = FindObjectOfType<Treasury>();
            chestUI = GetComponentInChildren<Canvas>().gameObject;
            artifactSelector = FindObjectOfType<ArtifactSelector>();
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
