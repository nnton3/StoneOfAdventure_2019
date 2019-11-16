using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class KnightAttackerFighter : Fighter
{
    //private Flip flip;
    //[SerializeField] private float attackRange = 1f;
    //[SerializeField] private int attackedLayer = 8;
    //[SerializeField] private float damage = 10f;

    //protected override void Start()
    //{
    //    base.Start();
    //    flip = GetComponent<Flip>();
    //}

    //public override void StartAttack()
    //{
    //    float chance = Random.Range(-1f, 1f);
    //    if (chance < 0f) anim.SetTrigger("attack1");
    //    else anim.SetTrigger("attack2");
    //}

    //// Animation event
    //public void Hit()
    //{
    //    Vector2 attackDirection = Vector2.right * ((flip.isFacingRight) ? 1 : -1);
    //    Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + 0.5f);
    //    Debug.Log("direction = " + attackDirection + ", rayOrigin = " + rayOrigin);
    //    RaycastHit2D[] hit = Physics2D.RaycastAll(rayOrigin, attackDirection, attackRange);

    //    foreach (var collider in hit)
    //    {
    //        if (collider.transform.CompareTag("Player"))
    //        {
    //            Debug.Log(collider.transform.GetComponent<IDamaged>() == null);
    //            collider.transform.GetComponent<IDamaged>().ApplyDamage(damage);
    //        }
    //    }
    //}
}
