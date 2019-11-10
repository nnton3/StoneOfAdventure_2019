using UnityEngine;
using StoneOfAdventure.Combat;
using System.Collections;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;

public class PlayerSkill2 : MonoBehaviour
{
    [SerializeField] private AttackCollider skillArea;
    [SerializeField] private float damage;
    [SerializeField] private float movespeed = 10f;
    [SerializeField] private float coolDown = 2f;


    private bool canUseSkill = true;
    public bool CanUseSkill => canUseSkill;
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerStateController unit;
    private Mover mover;
    private Jump jump;
    private Flip flip;

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        mover = GetComponent<Mover>();
        jump = GetComponent<Jump>();
        flip = GetComponent<Flip>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void StartUse()
    {
        canUseSkill = false;
        StartCoroutine("CoolDownTimer");
        anim.SetTrigger("skill2");
        float direction = (flip.isFacingRight) ? 1f : -1f;
        mover.MoveTo(direction, movespeed);
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canUseSkill = true;
    }

    public void Skill2End()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        mover.Cancel();
        unit.DisableState();
        if (!jump.isGrounded) { unit.Fell(); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.gameObject.CompareTag("Enemie"))
        {
            collision.gameObject.GetComponent<Health>().ApplyDamage(damage);
            Debug.Log("work");
        }
    }
}
