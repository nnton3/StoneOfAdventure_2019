using UnityEngine;
using StoneOfAdventure.UI;
using Zenject;

namespace StoneOfAdventure.Core
{
    public class Chest : MonoBehaviour
    {
        [Inject] private Treasury treasury;
        [Inject] private UIController uiController;

        [SerializeField] private int price = 10;
        private GameObject chestUI;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
            chestUI = GetComponentInChildren<Canvas>().gameObject;
        }

        // Animation event
        public void TryOpenChest()
        {
            if (price > treasury.CurrentSoulsPoints) return;
            anim.SetTrigger("use");
        }

        public void GiveReward() => uiController.ShowArtifactSelector();
    }
}
