using UnityEngine;
using System.Collections;
using System;

public class PaladinChaseBehaviour : ChaseBehaviour
{
    #region Variables
    [SerializeField] private float meleeAttackDistance = 1f;
    [SerializeField] private float rangeAttackDistance = 3f;

    private PaladinStateController paladin;
    private PaladinSkill1 skill1;
    private PaladinSkill2 skill2;
    private PaladinSkill3 skill3;
    private PaladinSkill4 skill4;
    #endregion

    protected override void Start()
    {
        base.Start();

        paladin = GetComponent<PaladinStateController>();
        skill1 = GetComponent<PaladinSkill1>();
        skill2 = GetComponent<PaladinSkill2>();
        skill3 = GetComponent<PaladinSkill3>();
        skill4 = GetComponent<PaladinSkill4>();
    }

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

        if (skill3.CanUseSkill)
        {
            paladin.Skill3();
            return;
        }

        if (skill4.CanUseSkill)
        {
            paladin.Skill4();
            return;
        }
    }

    private bool CanAttackInRange()
    {
        Debug.Log("check range attack distance");
        return Mathf.Abs(transform.position.x - player.transform.position.x) <= rangeAttackDistance;
    }

    private bool CanAttackInMelee()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) <= meleeAttackDistance;
    }
}
