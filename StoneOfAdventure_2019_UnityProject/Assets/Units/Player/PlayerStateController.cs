using System;
using UnityEngine;
using StoneOfAdventure.Core;

public class PlayerStateController : Unit
{
    private PlayerIdleState idleState;
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float movespeedInTheAir = 5f;
    [SerializeField] private float jumpPower = 800f;
    [SerializeField] private float verticalMovespeed = 3f;

    [SerializeField] private string currentState = "";

    private void Start()
    {
        State = GetComponent<PlayerIdleState>();
        idleState = GetComponent<PlayerIdleState>();
    }

    private void Update()
    {
        MoveHorizontal(Input.GetAxisRaw("Horizontal"));
        MoveVertical(Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Fire1") != 0f) Attack();
        if (Input.GetAxisRaw("Fire2") != 0f) Skill1();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        currentState = State.ToString();
    }

    public IPlayerState State { get; set; }

    private void Idle()
    {
        State.Idle();
    }

    private void MoveHorizontal(float direction)
    {
        State.MoveHorizontal(direction, (State == GetComponent<PlayerJumpState>()) ? movespeedInTheAir : movespeed);
    }

    private void MoveVertical(float direction)
    {
        State.MoveVertical(direction, verticalMovespeed);
    }

    private void Attack()
    {
        State.Attack();
    }

    private void Skill1()
    {
        State.Skill1();
    }

    private void Jump()
    {
        State.Jump(jumpPower);
    }

    public override void DisableState()
    {
        State = idleState;
    }

    internal void PlayerFell()
    {
        State.Fell();
    }
}
