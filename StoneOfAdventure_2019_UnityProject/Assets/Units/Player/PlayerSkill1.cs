using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;

public class PlayerSkill1 : SkillBase
{
    [SerializeField] private int damage;
    [SerializeField] private float timeOfStun;
   
    private Flip flip;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        flip = GetComponent<Flip>();
    }

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
            enemie.GetComponent<Unit>().ApplyStun(timeOfStun);
            enemie.GetComponent<Health>().ApplyDamage(damage);
        }
    }

    #region Application area params
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
    #endregion
}
