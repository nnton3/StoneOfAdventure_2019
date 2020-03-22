using System.Collections;
using UnityEngine;
using StoneOfAdventure.Movement;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class SaveEffortBoots_buff : MonoBehaviour
    {
        #region Variables
        private Health health;
        private Mover mover;

        private bool buffIsActive;
        private float buffTime;
        private float timeToApply;
        private float movespeedGain;
        [Inject] private DiContainer Container;
        #endregion

        public void Initialize(float buffTime, float timeToApply, float movespeedGain)
        {
            this.buffTime = buffTime;
            this.timeToApply = timeToApply;
            this.movespeedGain = movespeedGain;

            Container.Inject(this);
        }

        private void Start()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            health.HPDecreased.AddListener(ResetTimer);
            StartCoroutine("TimeWithoutDamage");
        }

        private void ResetTimer()
        {
            StopCoroutine("TimeWithoutDamage");
            if (buffIsActive)
                mover.ModifyMovespeedScale(-movespeedGain);    

            buffIsActive = false;
            StartCoroutine("TimeWithoutDamage");
        }

        IEnumerator TimeWithoutDamage()
        {
            yield return new WaitForSeconds(timeToApply);
            buffIsActive = true;
            mover.ModifyMovespeedScale(movespeedGain);
        }
    }
}
