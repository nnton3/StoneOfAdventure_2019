using UnityEngine;
using StoneOfAdventure.Combat;

public class ZombieFighter : Fighter
{
    private Flip flip;
    [SerializeField] private float attackRange;
    [SerializeField] private int attackedLayer;
    [SerializeField] private float damage;

    protected override void Start()
    {
        base.Start();
        flip = GetComponent<Flip>();
    }

    // Animation event
    public void Hit()
    {
        Vector2 attackDirection = Vector2.right;// * ((flip.isFacingRight) ? 1 : -1);
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + 0.5f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, attackDirection, attackRange, attackedLayer);
        
        if (hit)
        {
            Debug.Log(hit.transform.GetComponent<IDamaged>() == null);
            hit.transform.GetComponent<IDamaged>().ApplyDamage(damage);
        }
    }
}
