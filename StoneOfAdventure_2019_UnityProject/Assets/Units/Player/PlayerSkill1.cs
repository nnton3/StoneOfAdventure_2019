using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;

public class PlayerSkill1 : MonoBehaviour
{
    [SerializeField] private AttackCollider skillArea;
    [SerializeField] private float damage;
    [SerializeField] private float timeOfStun;
    [SerializeField] private float coolDown = 2f;

    private bool canUseSkill = true;
    public bool CanUseSkill => canUseSkill;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartUse()
    {
        canUseSkill = false;
        StartCoroutine("CoolDownTimer");
        anim.SetTrigger("skill1");
    }

    // Animation event
    public void Skill1Hit()
    {
        foreach (var enemie in skillArea.EnemieList)
        {
            enemie.GetComponent<Unit>().ApplyStun(timeOfStun);
            enemie.ApplyDamage(damage);
        }
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canUseSkill = true;
    }
}
