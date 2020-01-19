using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;

public class PaladinSkill1 : SkillBase
{
    #region
    private Flip flip;
    private Animator anim;
    [SerializeField] private GameObject patronPref;
    private float fireDirection = 1f;
    #endregion

    private void Start()
    {
        flip = GetComponent<Flip>();
        anim = GetComponent<Animator>();
    }

    public override void StartUse()
    {
        base.StartUse();
        fireDirection = (flip.isFacingRight) ? 1f : -1f;
        anim.SetTrigger("rangeAttack");
    }

    // Animation event
    public void InstancePatrons()
    {
        var instancePosition = new Vector3( transform.position.x,
                                            transform.position.y + 0.5f, 
                                            transform.position.z);
        var patron = Instantiate(patronPref, instancePosition, Quaternion.identity);
        patron.GetComponent<Mover>().MoveTo(fireDirection);
    }
}
