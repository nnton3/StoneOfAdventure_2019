using System.Collections.Generic;
using UnityEngine;

namespace StoneOfAdventure.Core
{
    public class SoulShardsPool : MonoBehaviour
    {
        private GameObject soulShardPref;
        private List<GameObject> soulShardPool = new List<GameObject>();

        public void FillPool(int value)
        {
            soulShardPref = Resources.Load<GameObject>("Soul_shard");

            var parent = GameObject.Find("SoulShards");
            if (parent == null)
                parent = Instantiate(new GameObject("SoulShards"));

            for (int i = 0; i < value; i++)
            {
                var soulShard = Instantiate(soulShardPref, parent.transform);
                soulShardPool.Add(soulShard);
                soulShard.SetActive(false);
            }
        }

        public List<GameObject> GetSoulShards()
        {
            return soulShardPool;
        }
    }
}
