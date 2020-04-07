using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace StoneOfAdventure.Core
{
    public class SoulsChest : MonoBehaviour
    {
        [Inject(Id = "SoulShard")] private ObjectPool soulShardPool;
        [SerializeField] private int reward = 3;

        protected virtual void Start()
        {
            soulShardPool = GetComponent<ObjectPool>();
            soulShardPool.FillPool(reward);

            GetComponentInChildren<Button>().onClick.AddListener(CreateReward);
        }

        public void CreateReward()
        {
            for (int i = 0; i < reward; i++)
            {
                var soulShard = soulShardPool.GetObject();
                soulShard.SetActive(true);
                soulShard.transform.position = transform.position;
            }
        }
    }
}
