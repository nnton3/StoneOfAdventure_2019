using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;

public class PatronBase : MonoBehaviour
{
    [SerializeField] private int damage;

    public void SetDirection(float direction)
    {
        GetComponent<Mover>().MoveTo(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}
