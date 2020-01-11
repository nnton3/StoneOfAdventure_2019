using UnityEngine;
using System.Collections;

public class PaladinSkill2 : SkillBase
{
    #region Variables
    private Animator anim;
    private Transform target;
    [SerializeField] private GameObject meteorPref;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerStateController>().transform;
    }

    public override void StartUse()
    {
        base.StartUse();
        anim.SetTrigger("meteorAttack");
    }

    // Animation event
    public void InstanceMeteor()
    {
        Instantiate(meteorPref, target.position, Quaternion.identity);
    }
}
