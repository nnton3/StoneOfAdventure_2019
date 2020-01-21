using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;

public class MovespeedDebuff : BaseBuff
{
    [SerializeField] private int stucsValue;
    private float slowDownValueAStuc = 0.1f;
    private float stucLifeTime = 3f;
    private Mover mover;

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    public void Initialize(float _slowDownValueAStuc, float _stucLifeTime)
    {
        slowDownValueAStuc = _slowDownValueAStuc;
        stucLifeTime = _stucLifeTime;
    }

    public override void ApplyBuff()
    {
        StopCoroutine("DebuffLifeTime");
        Debug.Log($"mover.CurrentMovespeedScale - slowDownValueAStuc = {mover.CurrentMovespeedScale - slowDownValueAStuc}");
        if ((mover.CurrentMovespeedScale - slowDownValueAStuc) > 0.1f)
        {
            stucsValue++;
            mover.ModifyMovespeedScale(slowDownValueAStuc * -1f);
        }
        StartCoroutine("DebuffLifeTime");
    }

    private IEnumerator DebuffLifeTime()
    {
        yield return new WaitForSeconds(stucLifeTime);
        RemoveBuff();
    }

    public override void RemoveBuff()
    {
        mover.ModifyMovespeedScale(stucsValue * slowDownValueAStuc);
        Destroy(this);
    }
}
