using UnityEngine;
using System.Collections;

public class SkillBase : MonoBehaviour
{
    [SerializeField] private float coolDown = 2f;

    private bool canUseSkill = true;
    public bool CanUseSkill => canUseSkill;

    public virtual void StartUse()
    {
        canUseSkill = false;
        StartCoroutine("CoolDownTimer");
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canUseSkill = true;
    }
}
