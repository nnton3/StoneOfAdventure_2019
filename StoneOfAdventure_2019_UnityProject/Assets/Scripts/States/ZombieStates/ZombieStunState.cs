using UnityEngine;
using System.Collections;

public class ZombieStunState : MonoBehaviour, IZombieState
{
    private Animator anim;
    private ZombieStateController unit;
    private Stunned stunned;

    private ZombieDeathState deathState;

    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<ZombieStateController>();
        stunned = GetComponent<Stunned>();

        deathState = GetComponent<ZombieDeathState>();
    }

    public void Attack() { return; }

    public void Dead()
    {
        anim.SetTrigger("dead");
        stunned.Cancel();
        unit.State = deathState;
    }

    public void MoveHorizontal(float direction, float movespeed) { return; }

    public void Stun(float time) { return; }
}
