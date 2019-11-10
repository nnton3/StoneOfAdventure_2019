using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoulsPointsEnumerator : MonoBehaviour
{
    private TextMeshProUGUI soulsPoints;
    private Treasury treasury;

    private void Start()
    {
        soulsPoints = GetComponent<TextMeshProUGUI>();
        treasury = FindObjectOfType<Treasury>();

        treasury.TresureUpdated.AddListener(UpdateSoulsEnumerator);
    }

    private void UpdateSoulsEnumerator()
    {
        soulsPoints.text = treasury.CurrentSoulsPoints.ToString();
    }
}
