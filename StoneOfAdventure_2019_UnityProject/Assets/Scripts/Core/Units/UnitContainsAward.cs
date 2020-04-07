using UnityEngine;
using StoneOfAdventure.Combat;
using Zenject;

namespace StoneOfAdventure.Core
{
    public class UnitContainsAward : Unit
    {
        [Inject (Id = "SoulShard")] private ObjectPool soulShardPool;
        [Inject] private PlayerLevelObserver levelObserver;
        [Inject] DiContainer container;

        [SerializeField] private int reward = 3;
        [SerializeField] private int experience = 5;

        protected virtual void Start()
        {
            container.Inject(this);
        }

        protected void CreateReward()
        {
            levelObserver.UpdateExperienceValue(experience);
            for (int i = 0; i < reward; i++)
            {
                var soulShard = soulShardPool.GetObject();
                soulShard.transform.position = transform.position;
                soulShard.SetActive(true);
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
