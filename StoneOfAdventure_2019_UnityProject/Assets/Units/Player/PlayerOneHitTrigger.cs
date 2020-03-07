using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class PlayerOneHitTrigger : OneHitTrigger
    {
        [SerializeField] private string secondTargetTag = "Boss";

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (collision.CompareTag(secondTargetTag))
            {
                ApplyDamage(collision);
            }
        }
    }
}
