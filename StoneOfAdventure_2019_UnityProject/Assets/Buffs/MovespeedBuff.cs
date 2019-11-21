using StoneOfAdventure.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovespeedBuff : MonoBehaviour
{
    [SerializeField] private float movespeedGain = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            var currentMover = collision.GetComponent<Mover>();
            if (currentMover) currentMover.ModifyMovespeedScale(movespeedGain);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            var currentMover = collision.GetComponent<Mover>();
            if (!currentMover) return;
            if (currentMover.CurrentMovespeedScale - movespeedGain < 1f) return;

            currentMover.ModifyMovespeedScale(-movespeedGain);
        }
    }
}
