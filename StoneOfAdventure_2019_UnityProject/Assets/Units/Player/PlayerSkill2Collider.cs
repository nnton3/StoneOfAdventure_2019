using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class PlayerSkill2Collider : MonoBehaviour
{
    [HideInInspector] public int skill2Damage = 5;

    private void Start() { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
            collision.GetComponent<Health>().ApplyDamage(skill2Damage);
    }
}
