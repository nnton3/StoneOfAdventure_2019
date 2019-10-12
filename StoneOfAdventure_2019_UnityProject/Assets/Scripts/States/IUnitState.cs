using UnityEngine;
using System.Collections;

public interface IUnitState
{
    void Idle();
    void MoveHorizontal(float direction, float movespeed);
    void MoveVertical();
    void Attack();
    void Jump(float jumpPower);
    void Fell();
}
