using UnityEngine;
using Panda;

public class PaladinChaseBehaviour : ChaseBehaviour
{
    #region Variables
    [SerializeField] private float rangeAttackDistance = 3f;

    private PaladinStateController paladin;
    private PaladinSkill1 rangeAttack;
    private PaladinSkill2 meteor;
    private PaladinSkill3 fireJump;
    private PaladinSkill4 curse;
    #endregion

    protected override void Start()
    {
        base.Start();

        paladin = GetComponent<PaladinStateController>();
        rangeAttack = GetComponent<PaladinSkill1>();
        meteor = GetComponent<PaladinSkill2>();
        fireJump = GetComponent<PaladinSkill3>();
        curse = GetComponent<PaladinSkill4>();
    }

    private void RangeAttack()
    {
        paladin.RangeAttack();
    }

    private void MeleeAttack()
    {
        unit.Attack();
    }

}
