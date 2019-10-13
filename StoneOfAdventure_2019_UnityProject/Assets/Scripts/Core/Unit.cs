﻿using UnityEngine;
using System;

namespace StoneOfAdventure.Core
{
    public class Unit : MonoBehaviour
    {
        private IdleState idleState;
        [SerializeField] private float movespeed = 5f;
        [SerializeField] private float movespeedInTheAir = 5f;
        [SerializeField] private float jumpPower = 800f;
        [SerializeField] private float verticalMovespeed = 3f;

        [SerializeField] private string currentState = "";

        private void Start()
        {
            State = GetComponent<IdleState>();
            idleState = GetComponent<IdleState>();
        }

        private void Update()
        {
            MoveHorizontal(Input.GetAxisRaw("Horizontal"));
            MoveVertical(Input.GetAxisRaw("Vertical"));
            if (Input.GetAxisRaw("Fire1") != 0f) Attack();
            if (Input.GetAxisRaw("Jump") != 0f) Jump();

            currentState = State.ToString();
        }

        public IUnitState State { get; set; }

        private void Idle()
        {
            State.Idle();
        }

        private void MoveHorizontal(float direction)
        {
            State.MoveHorizontal(direction, (State == GetComponent<JumpState>()) ? movespeedInTheAir : movespeed);
        }

        private void MoveVertical(float direction)
        {
            State.MoveVertical(direction, verticalMovespeed);
        }

        private void Attack()
        {
            State.Attack();
        }

        private void Jump()
        {
            State.Jump(jumpPower);
        }

        public void DisableState()
        {
            State = idleState;
        }

        internal void PlayerFell()
        {
            State.Fell();
        }
    }
}
