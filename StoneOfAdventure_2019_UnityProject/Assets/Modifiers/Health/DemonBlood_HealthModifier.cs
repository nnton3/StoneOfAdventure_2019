using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using Zenject;

public class DemonBlood_HealthModifier : MonoBehaviour
{
    #region Variables
    private int maxStucsValue;
    private float effectTime;
    private float healPerSecPerStuc;
    private int currentStucsValue;

    private Health health;
    private HealthRegen healthRegen;
    [Inject] private DiContainer Container;
    #endregion

    public void Initialize(int _maxStucsValue, float _effectTime, float _healPerSecPerStuc)
    {
        maxStucsValue = _maxStucsValue;
        effectTime = _effectTime;
        healPerSecPerStuc = _healPerSecPerStuc;

        Container.Inject(this);
    }

    private void Start()
    {
        health = GetComponent<Health>();
        healthRegen = GetComponent<HealthRegen>();

        health.HPDecreased.AddListener(TryToAddStuc);
    }

    private void TryToAddStuc(int value)
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
