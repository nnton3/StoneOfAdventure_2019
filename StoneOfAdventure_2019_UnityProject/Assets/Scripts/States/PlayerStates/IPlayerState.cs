using UnityEngine;
using System.Collections;

public interface IPlayerState
{
    void Idle();
    void MoveHorizontal(float direction, float movespeed);
    void MoveVertical(float direction, float verticalMovespeed);
    void Attack();
    void Jump(float jumpPower);
    void Fell();
    void Skill1();
}
