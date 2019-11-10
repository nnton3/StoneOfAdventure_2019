using UnityEngine;
using System.Collections;

namespace StoneOfAdventure.Core
{
    public class UnitContainsAward : Unit
    {
        private GameObject soulShard;

        [SerializeField] private int reward = 3;

        protected virtual void Start()
        {
            soulShard = Resources.Load<GameObject>("Soul_shard");
        }

        protected void CreateReward()
        {
            for (int i = 0; i < reward; i++)
            {
                Instantiate(soulShard, transform.position, Quaternion.identity);
            }
        }
    }
}
