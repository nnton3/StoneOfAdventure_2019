using UnityEngine;
using System.Collections;

public class PaladinSkill2 : SkillBase
{
    #region Variables
    private Animator anim;
    private Flip flip;
    private Transform target;
    [SerializeField] private GameObject meteorPref;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        flip = GetComponent<Flip>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void StartUse()
    {
        base.StartUse();
        if ((target.position.x > transform.position.x && !flip.isFacingRight) ||
            (target.position.x < transform.position.x && flip.isFacingRight))
            flip.FlipObject();
        anim.SetTrigger("meteorAttack");
    }

    // Animation event
    public void InstanceMeteor()
    {
        Instantiate(meteorPref, new Vector3(target.position.x, 0f, 0f), Quaternion.identity);
    }
}
