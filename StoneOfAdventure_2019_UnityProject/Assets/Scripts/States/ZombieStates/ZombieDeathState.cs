using UnityEngine;
using System.Collections;

public class ZombieDeathState : MonoBehaviour, IZombieState
{
    public void Attack() { return; }

    public void Dead() { return; }

    public void MoveHorizontal(float direction, float movespeed) { return; }

    public void Stun(float time) { return; }
}
