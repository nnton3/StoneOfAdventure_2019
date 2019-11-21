using UnityEngine;
using System.Collections;

public class testDelegate : MonoBehaviour
{
    private float baseDamage;

    private delegate void SummaryDamage(ref float _currentDamage);
    private SummaryDamage addedDamage;

    private void Start()
    {
        addedDamage += AddDamage1;
        addedDamage += AddDamage2;
        addedDamage += AddDamage3;

        float currentDamage = baseDamage; 
        addedDamage(ref currentDamage);

        Debug.Log($"Current damage = {currentDamage}");
    }

    private void AddDamage1(ref float _currentDamage)
    {
        Debug.Log($"Add 5 damage");
        _currentDamage += 5f;
    }

    private void AddDamage2(ref float _currentDamage)
    {
        Debug.Log($"Add 1 damage");
        _currentDamage += 1f;
    }

    private void AddDamage3(ref float _currentDamage)
    {
        Debug.Log($"Add 3 damage");
        _currentDamage += 3f;
    }
}
