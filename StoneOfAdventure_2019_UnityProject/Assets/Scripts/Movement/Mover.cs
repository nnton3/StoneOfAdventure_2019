using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoneOfAdventure.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Flip))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class Mover : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Animator anim;
        protected Flip flip;
        [SerializeField] protected float movespeed;

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            flip = GetComponent<Flip>();
        }

        internal void MoveTo(float direction)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = Vector2.right * direction * movespeed;
            rb.AddForce(horizontalMove, ForceMode2D.Force);
        }
    }
}
