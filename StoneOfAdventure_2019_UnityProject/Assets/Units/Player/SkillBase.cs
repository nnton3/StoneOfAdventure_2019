using UnityEngine;
using System.Collections;

public class SkillBase : MonoBehaviour
{
    [SerializeField] private float coolDown = 2f;
    [SerializeField] protected int baseDamage = 10;

    private bool canUseSkill = true;
    public bool CanUseSkill => canUseSkill;

    public virtual void StartUse()
    {
        canUseSkill = false;
        StartCoroutine("CoolDownTimer");
    }

    public void IncreaseBaseDamage(float increaseDamage)
    {
        baseDamage += (int)(baseDamage * increaseDamage);
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canUseSkill = true;
    }
}
