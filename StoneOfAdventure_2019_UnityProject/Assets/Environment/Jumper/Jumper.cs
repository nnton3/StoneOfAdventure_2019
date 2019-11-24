using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpPower = 3000;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var jumpScript = collision.gameObject.GetComponent<Jump>();
        if (jumpScript)
        {
            collision.GetComponent<Unit>().Fell();
            jumpScript.ToJump(Vector2.up, jumpPower);
        }
    }
}
