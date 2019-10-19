
using UnityEngine;
using System.Collections;

public interface IZombieState
{
    void Attack();
    void MoveHorizontal(float direction, float movespeed);
    void Dead();
    void Stun(float time);
}
