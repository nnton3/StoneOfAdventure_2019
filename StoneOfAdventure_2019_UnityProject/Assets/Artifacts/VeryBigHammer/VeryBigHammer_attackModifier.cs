﻿using StoneOfAdventure.Core;
using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class VeryBigHammer_attackModifier : MonoBehaviour
    {
        #region Variables
        private Fighter fighter;

        private float chance;
        private float timeInStun;
        private int damage;
        [Inject] private DiContainer Container;
        #endregion

        public void Initialize(float chance, float timeInStun, int damage)
        {
            this.chance = chance;
            this.timeInStun = timeInStun;
            this.damage = damage;

            Container.Inject(this);
        }

        private void Start()
        {
            fighter = GetComponent<Fighter>();

            fighter.AddEffectOfAttack(TryToStun);
        }

        private void TryToStun(GameObject target)
        {
            var chance = Random.Range(0f, 1f);
            if (chance < this.chance)
            {
                target.GetComponent<Unit>().ApplyStun(timeInStun);
                target.GetComponent<Health>().ApplyDamage(damage);
            }
        }
    }
}
