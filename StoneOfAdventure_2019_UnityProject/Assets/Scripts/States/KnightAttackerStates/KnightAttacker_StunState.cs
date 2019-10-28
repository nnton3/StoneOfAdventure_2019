using UnityEngine;
using System.Collections;

public class KnightAttacker_StunState : MonoBehaviour, IKnightAttackerState
{
    public void Attack() { return; }

    public void Dead() { return; }

    public void MoveHorizontal(float direction, float movespeed) { return; }

    public void Stun(float time) { return; }

    public void Spurt() { return; }
}
