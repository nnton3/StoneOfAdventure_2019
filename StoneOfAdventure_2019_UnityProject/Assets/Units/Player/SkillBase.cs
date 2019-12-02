using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SkillBase : MonoBehaviour
{
    [SerializeField] private float coolDown = 2f;
    public float CoolDown => coolDown;
    [SerializeField] protected int baseDamage = 10;

    private bool canUseSkill = true;
    public bool CanUseSkill => canUseSkill;

    public UnityEvent SkillUsed;

    public virtual void StartUse()
    {
        canUseSkill = false;
        StartCoroutine("CoolDownTimer");
        SkillUsed.Invoke();
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
