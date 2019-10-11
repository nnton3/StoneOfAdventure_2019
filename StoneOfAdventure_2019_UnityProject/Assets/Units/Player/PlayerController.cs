using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movespeed;

    private void Start()
    {
        State = GetComponent<IdleState>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f) MoveHorizontal(Input.GetAxisRaw("Horizontal"));
        if (Input.GetAxisRaw("Vertical") != 0f) MoveVertical();
        if (Input.GetAxisRaw("Attack") != 0f) Attack();
        if (Input.GetAxisRaw("Jump") != 0f) Jump();
    }

    public IUnitState State { get; set; }

    private void Idle()
    {
        State.Idle();
    }

    private void MoveHorizontal (float direction)
    {
        State.MoveHorizontal(direction, movespeed);
    }

    private void MoveVertical()
    {
        State.MoveVertical();
    }

    private void Attack()
    {
        State.Attack();
    }

    private void Jump()
    {
        State.Jump();
    }
}
