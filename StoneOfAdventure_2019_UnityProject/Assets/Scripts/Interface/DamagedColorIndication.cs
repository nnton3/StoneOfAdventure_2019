using StoneOfAdventure.Combat;
using UnityEngine;

public class DamagedColorIndication : MonoBehaviour
{
    private Health health;
    private Animator anim;

    private void Start()
    {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();

        health.HPDecreased.AddListener(SwapColor);
    }

    private void SwapColor(int value)
    {
        anim.SetTrigger("damaged");
    }
}
