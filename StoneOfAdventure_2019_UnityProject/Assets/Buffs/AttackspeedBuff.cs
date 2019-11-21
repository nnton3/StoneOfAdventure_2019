using StoneOfAdventure.Combat;
using UnityEngine;

public class AttackspeedBuff : MonoBehaviour
{
    [SerializeField] private float attackspeedGain = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            var currentFighter = collision.GetComponent<Fighter>();
            if (currentFighter) currentFighter.ModifyAttackSpeed(attackspeedGain);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            var currentFighter = collision.GetComponent<Fighter>();
            if (!currentFighter) return;
            if (currentFighter.CurrentAttackSpeed - attackspeedGain < 1f) return;

            currentFighter.ModifyAttackSpeed(-attackspeedGain);
        }
    }
}
