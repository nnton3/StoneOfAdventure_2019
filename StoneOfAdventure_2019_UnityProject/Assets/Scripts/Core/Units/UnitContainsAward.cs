using UnityEngine;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Core
{
    public class UnitContainsAward : Unit
    {
        private GameObject soulShardPref;
        private SoulShardsPool soulShardPool;
        private PlayerLevelObserver levelObserver;

        [SerializeField] private int reward = 3;
        public int Reward => reward;

        [SerializeField] private int experience = 5;

        protected virtual void Start()
        {
            soulShardPool = GetComponent<SoulShardsPool>();
            levelObserver = FindObjectOfType<PlayerLevelObserver>();

            soulShardPool.FillPool(reward);
        }

        protected void CreateReward()
        {
            levelObserver.UpdateExperienceValue(experience);
            var soulShards = soulShardPool.GetSoulShards();
            for (int i = 0; i < reward; i++)
            {
                soulShards[i].SetActive(true);
                soulShards[i].transform.position = transform.position;
            }
        }

        protected void ReturnToPool()
        {
            GetComponent<Health>().ResetParams();
            gameObject.SetActive(false);
            DisableState();
        }
    }
}
