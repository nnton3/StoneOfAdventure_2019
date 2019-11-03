using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointsUI : MonoBehaviour
{
    private Health health;
    private float lastHealthSave;
    private Flip unitFlip;
    private Flip thisFlip;

    private void Start()
    {
        health = GetComponentInParent<Health>();
        unitFlip = GetComponentInParent<Unit>().GetComponent<Flip>();
        thisFlip = GetComponent<Flip>();

        unitFlip.Flipped.AddListener(FixXScale);
        lastHealthSave = health.HealthPoints;
        health.HPUpdated.AddListener(CalculateHealthDifference);
    }

    private void FixXScale()
    {
        Debug.Log("fix");
        thisFlip.CheckDirection((unitFlip.isFacingRight) ? 1f : -1f);
    }

    private void CalculateHealthDifference()
    {
        if (lastHealthSave - health.HealthPoints != 0f) CreateDamagePointsUI($"{health.HealthPoints - lastHealthSave}");
        lastHealthSave = health.HealthPoints;
    }

    [SerializeField] private GameObject pointsUI;
    private void CreateDamagePointsUI(string text)
    {
        GameObject currentInstance = Instantiate(pointsUI, Vector3.zero, Quaternion.identity, transform);
        currentInstance.transform.localPosition = Vector3.zero;
        currentInstance.GetComponent<Text>().text = text;
        currentInstance.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 3f, ForceMode2D.Impulse);
     }
}
