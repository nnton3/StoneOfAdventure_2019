using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class DemonBlood_HealthModifier : MonoBehaviour
{
    #region Variables
    internal int maxStucsValue;
    internal float effectTime;
    internal float healPerSecPerStuc;

    private int currentStucsValue;

    private Health health;
    private HealthRegen healthRegen;
    #endregion
    private void Start()
    {
        health = GetComponent<Health>();
        healthRegen = GetComponent<HealthRegen>();

        health.HPDecreased.AddListener(TryToAddStuc);
    }

    private void TryToAddStuc()
    {
        if (currentStucsValue < maxStucsValue) currentStucsValue++;
        healthRegen.SetTemporaryRegen(currentStucsValue * healPerSecPerStuc, effectTime);
    }

    IEnumerator StucTimer()
    {
        yield return new WaitForSeconds(effectTime);
        currentStucsValue = 0;
    }
}
