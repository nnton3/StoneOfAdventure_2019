using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;

public class PlayerSkill1 : MonoBehaviour
{
    [SerializeField] private AttackCollider skillArea;
    [SerializeField] private float damage;
    [SerializeField] private float timeOfStun;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartUse()
    {
        anim.SetTrigger("skill1");
    }

    public void Skill1Hit()
    {
        foreach (var enemie in skillArea.EnemieList)
        {
            enemie.GetComponent<Unit>().ApplyStun(timeOfStun);
            enemie.ApplyDamage(damage);
        }
    }
}
