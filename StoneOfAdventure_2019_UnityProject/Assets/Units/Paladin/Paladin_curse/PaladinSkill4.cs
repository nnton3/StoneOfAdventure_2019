using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSkill4 : SkillBase
{
    #region Variables
    [SerializeField] private float slowInPercent;
    [SerializeField] private float actionTime;
    private Animator anim;
    private GameObject target;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerStateController>().gameObject;
    }

    public override void StartUse()
    {
        base.StartUse();

        anim.SetTrigger("curse");
    }

    public void ApplyCurse()
    {
        var movespeedDebuff = target.AddComponent<MovespeedDebuff>();
        movespeedDebuff.Initialize(slowInPercent, actionTime);
        movespeedDebuff.ApplyBuff();
    }
}
