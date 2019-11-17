using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class SphereOfEnergy_AttackModifier : MonoBehaviour
{
    #region Variables
    private PlayerFighter playerFighter;
    private PlayerStateController playerController;
    public float targetTimeOnFeet = 2f;
    public float bonusDamageInPercent = 1.5f;
    [SerializeField] private float currentTimeOnFeet = 0f;
    [SerializeField] private float minInterval = 0.2f;
    #endregion

    private void Start()
    {
        playerFighter = GetComponent<PlayerFighter>();
        playerController = GetComponent<PlayerStateController>();

        playerController.StartWalk.AddListener(StartOnFeetTimer);
        playerController.StopWalk.AddListener(StopOnFeetTimer);
        playerFighter.Attack.AddListener(CalculateDamageScale);
    }

    private void StartOnFeetTimer()
    {
        InvokeRepeating("IncrementCurrentTimeInFeet", 0f, minInterval);
    }

    private void StopOnFeetTimer()
    {
        CancelInvoke("IncrementCurrentTimeInFeet");
    }

    private void IncrementCurrentTimeInFeet()
    {
        currentTimeOnFeet += minInterval;
    }

    private void CalculateDamageScale()
    {
        if (currentTimeOnFeet >= targetTimeOnFeet)
        {
            playerFighter.SetDamageScaleForNexAttack(bonusDamageInPercent);
            currentTimeOnFeet = 0f;
        }
    }
}
