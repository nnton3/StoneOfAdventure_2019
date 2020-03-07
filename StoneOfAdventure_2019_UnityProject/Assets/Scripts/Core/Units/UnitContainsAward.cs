using UnityEngine;
using Assets.Scripts.Core;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Core
{
    public class UnitContainsAward : Unit
    {
        private GameObject soulShard;
        private PlayerLevelObserver levelObserver;

        [SerializeField] private int reward = 3;
        [SerializeField] private int experience = 5;

        protected virtual void Start()
        {
            soulShard = Resources.Load<GameObject>("Soul_shard");
            levelObserver = FindObjectOfType<PlayerLevelObserver>();
        }

        protected void CreateReward()
        {
            levelObserver.UpdateExperienceValue(experience);
            for (int i = 0; i < reward; i++)
            {
                Instantiate(soulShard, transform.position, Quaternion.identity);
            }
        }

        protected void ReturnToPool()
        {
            gameObject.SetActive(false);
            GetComponent<Health>().ResetParams();
        }
    }
}
