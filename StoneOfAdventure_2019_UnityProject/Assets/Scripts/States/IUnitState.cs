using UnityEngine;
using System.Collections;

public interface IUnitState
{
    void Idle();
    void MoveHorizontal(float direction, float movespeed);
    void MoveVertical(float direction, float verticalMovespeed);
    void Attack();
    void Jump(float jumpPower);
    void Fell();
}
