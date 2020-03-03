using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class PaladinSkill4 : SkillBase
    {
        #region Variables
        [SerializeField] private float slowInPercent;
        [SerializeField] private float actionTime;
        private Animator anim;
        private Flip flip;
        private GameObject target;
        #endregion

        private void Start()
        {
            anim = GetComponent<Animator>();
            flip = GetComponent<Flip>();
            target = FindObjectOfType<PlayerStateController>().gameObject;
        }

        public override void StartUse()
        {
            base.StartUse();

            if ((target.transform.position.x > transform.position.x && !flip.isFacingRight) ||
                        (target.transform.position.x < transform.position.x && flip.isFacingRight))
                flip.FlipObject();
            anim.SetTrigger("curse");
        }

        public void ApplyCurse()
        {
            var movespeedDebuff = target.AddComponent<MovespeedDebuff>();
            movespeedDebuff.Initialize(slowInPercent, actionTime);
            movespeedDebuff.ApplyBuff();
        }
    }
}
