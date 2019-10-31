using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    public virtual void Idle() { return; }
    public virtual void MoveHorizontal(float direction, float movespeed) { return; }
    public virtual void MoveVertical(float direction, float verticalMovespeed) { return; }
    public virtual void Attack() { return; }
    public virtual void Jump(float jumpPower) { return; }
    public virtual void Fell() { return; }
    public virtual void Skill1() { return; }
    public virtual void Skill2() { return; }
    public virtual void Dead() { return; }
    public virtual void Stun(float time) { return; }
}
