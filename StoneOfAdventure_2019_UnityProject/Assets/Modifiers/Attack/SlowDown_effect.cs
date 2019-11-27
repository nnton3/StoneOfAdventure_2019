using StoneOfAdventure.Combat;
using UnityEngine;

public class SlowDown_effect : BaseBuff
{
    private Fighter fighter;
    private float slowInPercent = 0.5f;
    private float actionTime = 3f;

    private void Start()
    {
        fighter = GetComponent<Fighter>();

        fighter.AddEffectOfAttack(SlowDown);
    }

    public void Initialize(float _slowInPercent, float _actionTime)
    {
        slowInPercent = _slowInPercent;
        actionTime = _actionTime;
    }

    private void SlowDown(GameObject target)
    {
        var debuff = target.GetComponent<MovespeedDebuff>();
        if (!debuff)
        {
            var addedMovespeedDebuff = target.AddComponent<MovespeedDebuff>();
            addedMovespeedDebuff.Initialize(slowInPercent, actionTime);
            addedMovespeedDebuff.ApplyBuff();
        }
        else
        {
            debuff.ApplyBuff();
        }
    }

    public override void ApplyBuff()
    {
        base.ApplyBuff();
    }

    public override void RemoveBuff()
    {
        Debug.Log("remove attack effect");
        fighter.RemoveEffectOfAttack(SlowDown);
        Destroy(this);
    }
}
