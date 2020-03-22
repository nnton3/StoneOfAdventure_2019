using UnityEngine;
using StoneOfAdventure.Combat;
using Zenject;
using System.Collections;

public class TrackingEye_HealthModifier : MonoBehaviour
{
    #region Variables
    private float timeToBlock = 3;
    [Inject(Id = "Player")] private Health health;
    [Inject] private DiContainer Container;
    private bool blockIsReady;
    #endregion

    public void Initialize(float timeToBlock)
    {
        this.timeToBlock = timeToBlock;

        Container.Inject(this);
    }

    private void Start()
    {
        health.AddModifierOfInputDamage(TryToBlockNextAttack);
        StartCoroutine("TimeToBlockTimer");
    }

    private IEnumerator TimeToBlockTimer()
    {
        yield return new WaitForSeconds(timeToBlock);
        blockIsReady = true;
    }

    private void TryToBlockNextAttack(ref int damage)
    {
        if (blockIsReady)
        {
            damage = 0;
        }
        blockIsReady = false;
        StartCoroutine("TimeToBlockTimer");
    }
}
