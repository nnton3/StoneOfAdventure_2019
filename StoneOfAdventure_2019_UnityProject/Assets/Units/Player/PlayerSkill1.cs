using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;

public class PlayerSkill1 : MonoBehaviour
{
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
        Collider[] enemiesInApplicationArea = Physics.OverlapBox(transform.position + applicationAreaCenter, applicationArea / 2, Quaternion.identity);
        foreach (var enemie in enemiesInApplicationArea)
        {
            enemie.GetComponent<Unit>().ApplyStun(timeOfStun);
            enemie.GetComponent<Health>().ApplyDamage(damage);
        }
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canUseSkill = true;
    }

    [SerializeField] private Vector3 applicationAreaCenter;
    [SerializeField] private Vector3 applicationArea;
    [SerializeField] private bool applicationAreaVisible;
    [SerializeField] private LayerMask layerMask;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (applicationAreaVisible)
            Gizmos.DrawWireCube(transform.position + applicationAreaCenter, applicationArea);
    }
}
