using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class AttackState : MonoBehaviour, IUnitState
{
    private Fighter fighter;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
    }

    public void Attack()
    {
        return;
    }

    public void Idle()
    {
        return;
    }

    public void Jump()
    {
        return;
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        return;
    }

    public void MoveVertical()
    {
        return;
    }
}
