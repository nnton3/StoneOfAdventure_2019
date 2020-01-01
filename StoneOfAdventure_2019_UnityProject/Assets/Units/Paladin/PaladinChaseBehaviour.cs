using UnityEngine;
using System.Collections;
using System;

public class PaladinChaseBehaviour : ChaseBehaviour
{
    #region Variables
    [SerializeField] private float meleeAttackDistance = 1f;
    [SerializeField] private float rangeAttackDistance = 3f;

    private PaladinSkill1 skill1;
    private PaladinSkill2 skill2;
    #endregion

    public override void UpdateChaseBehaviour()
    {
        if (CanAttackInMelee())
        {
            unit.Attack();
            return;
        }

        if (CanAttackInRange())
        {
            if (skill1.CanUseSkill)
            {
                unit.Skill1();
                return;
            }
        }

        if (skill2.CanUseSkill)
        {
            unit.Skill2();
            return;
        }
    }

    private bool CanAttackInRange()
    {
        return (transform.position.x - player.transform.position.x) <= rangeAttackDistance;
    }

    private bool CanAttackInMelee()
    {
        return (transform.position.x - player.transform.position.x) <= meleeAttackDistance;
    }
}
