using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Treasury : MonoBehaviour
{
    private int currentSoulsPoints;
    public int CurrentSoulsPoints => currentSoulsPoints;
    [HideInInspector] public UnityEvent TresureUpdated;

    public void Refill(int soulsPoints)
    {
        if (soulsPoints > 0) currentSoulsPoints += soulsPoints;
        TresureUpdated.Invoke();
    }

    public void Spend (int soulsPoints)
    {
        if (soulsPoints > 0) currentSoulsPoints -= soulsPoints;
        TresureUpdated.Invoke();
    }

    private void OnDisable()
    {
        TresureUpdated.RemoveAllListeners();
    }
}
