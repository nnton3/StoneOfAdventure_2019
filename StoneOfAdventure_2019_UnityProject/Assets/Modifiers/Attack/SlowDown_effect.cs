using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown_effect : MonoBehaviour
{
    private Fighter fighter;
    [SerializeField] private float slowInPercent = 0.5f;
    [SerializeField] private float actionTime = 3f;

    private void Start()
    {
        fighter.AddEffectOfAttack(SlowDown);
    }

    private void SlowDown(GameObject target)
    {
        var targetMover = target.GetComponent<Mover>();
        targetMover.ModifyMovespeedScale(slowInPercent);
    }
}
