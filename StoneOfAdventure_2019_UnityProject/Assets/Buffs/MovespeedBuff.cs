using StoneOfAdventure.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovespeedBuff : BaseBuff
{
    private float movespeedGain = 0.5f;
    private Mover mover;

    public void Initialize(float _movespeedGain)
    {
        movespeedGain = _movespeedGain;
    }

    private void Awake()
    {
        mover = GetComponent<Mover>();
    }

    public override void ApplyBuff()
    {
        mover.ModifyMovespeedScale(movespeedGain);
        base.ApplyBuff();
    }

    public override void RemoveBuff()
    {
        mover.ModifyMovespeedScale(-movespeedGain);
        Destroy(this);
    }
}
