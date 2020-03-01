using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
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
        unitFlip = GetComponentInParent<Flip>();
        thisFlip = GetComponent<Flip>();

        unitFlip.Flipped.AddListener(FixXScale);
        lastHealthSave = health.HealthPoints;
        health.HPDecreased.AddListener(CalculateHealthDifference);
    }

    private void FixXScale()
    {
        thisFlip.CheckDirection((unitFlip.isFacingRight) ? 1f : -1f);
    }

    private void CalculateHealthDifference()
    {
        var healthDifference = lastHealthSave - health.HealthPoints;
        if (healthDifference == 0f) return;
        if (healthDifference > 0) CreatePointsUI($"{healthDifference}", Color.red);
        if (healthDifference < 0) CreatePointsUI($"{healthDifference}", Color.green);
        lastHealthSave = health.HealthPoints;
    }

    [SerializeField] private GameObject pointsUI;
    private void CreatePointsUI(string text, Color pointsColor)
    {
        GameObject currentInstance = Instantiate(pointsUI, Vector3.zero, Quaternion.identity, transform);
        currentInstance.transform.localPosition = Vector3.zero;
        var textComponent = currentInstance.GetComponent<Text>();
        textComponent.text = text;
        textComponent.color = pointsColor;
        currentInstance.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 3f, ForceMode2D.Impulse);
     }
}
