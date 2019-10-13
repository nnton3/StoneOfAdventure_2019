using UnityEngine;
using System.Collections;

public class ZombieDeathState : MonoBehaviour, IZombieState
{
    private void Start()
    {
        GetComponent<Collider>();
    }

    public void Attack() { return; }

    public void Dead() { return; }

    public void MoveHorizontal(float direction, float movespeed) { return; }
}
