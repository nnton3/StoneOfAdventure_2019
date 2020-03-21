using UnityEngine;
using StoneOfAdventure.Core;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class PlayerSkill1 : SkillBase
    {
        [SerializeField] private float timeOfStun;

        [Inject(Id = "Player")] private Flip flip;
        [Inject(Id = "Player")] private Animator anim;

        public override void StartUse()
        {
            base.StartUse();
            anim.SetTrigger("skill1");
        }

        // Animation event
        public void Skill1Hit()
        {
            Vector2 centerInRelationUnitDirection =
                    transform.position + applicationAreaCenter * ((flip.isFacingRight) ? 1 : -1);
            Collider2D[] enemiesInApplicationArea = Physics2D.OverlapBoxAll(
                            centerInRelationUnitDirection,
                            applicationArea,
                            0f,
                            layerMask);
            foreach (var enemie in enemiesInApplicationArea)
            {
                enemie.GetComponent<Health>().ApplyDamage(baseDamage);
                enemie.GetComponent<Unit>().ApplyStun(timeOfStun);
            }
        }

        [SerializeField] private Vector3 applicationAreaCenter;
        [SerializeField] private Vector3 applicationArea;
        [SerializeField] private bool applicationAreaVisible;
        [SerializeField] private LayerMask layerMask;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (applicationAreaVisible)
                Gizmos.DrawWireCube(transform.position + applicationAreaCenter, applicationArea);
        }
    }
}
