using UnityEngine;
using System.Collections;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class PaladinOneHitTrigger : OneHitTrigger
    {
        protected override void ApplyDamage(Collider2D collision)
        {
            base.ApplyDamage(collision);
            StunEffect(collision.gameObject);
        }

        private void StunEffect(GameObject player)
        {
            player.GetComponent<Unit>().ApplyStun(0f);
        }
    }
}
