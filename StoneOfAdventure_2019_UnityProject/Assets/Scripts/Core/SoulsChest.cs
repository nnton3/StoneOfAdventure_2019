using UnityEngine;
using UnityEngine.UI;

namespace StoneOfAdventure.Core
{
    public class SoulsChest : MonoBehaviour
    {
        private GameObject soulShardPref;
        private SoulShardsPool soulShardPool;
        [SerializeField] private int reward = 3;

        protected virtual void Start()
        {
            soulShardPool = GetComponent<SoulShardsPool>();
            soulShardPool.FillPool(reward);

            GetComponentInChildren<Button>().onClick.AddListener(CreateReward);
        }

        public void CreateReward()
        {
            var soulShards = soulShardPool.GetSoulShards();
            for (int i = 0; i < reward; i++)
            {
                soulShards[i].SetActive(true);
                soulShards[i].transform.position = transform.position;
            }
        }
    }
}
