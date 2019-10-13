using UnityEngine;
using System.Collections;

public class ZombieAttackState : MonoBehaviour, IZombieState
{
    private Animator anim;
    private ZombieStateController unit;

    private ZombieDeathState deathState;

    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<ZombieStateController>();

        deathState = GetComponent<ZombieDeathState>();
    }

    public void Attack() { return; }

    public void Dead()
    {
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public void MoveHorizontal(float direction, float movespeed) { return; }
}
