﻿using StoneOfAdventure.Core;
using UnityEngine;

namespace StoneOfAdventure.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Flip))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class Mover : MonoBehaviour, IAction
    {
        protected Rigidbody2D rb;
        protected Animator anim;
        protected Flip flip;

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            flip = GetComponent<Flip>();
        }

        internal void MoveTo(float direction, float movespeed)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = Vector2.right * direction * movespeed;
            rb.velocity = horizontalMove;
            anim.SetBool("moveHorizontal", true);
        }

        public void Cancel()
        {
            rb.AddForce(Vector2.zero);
            anim.SetBool("moveHorizontal", false);
        }
    }
}
