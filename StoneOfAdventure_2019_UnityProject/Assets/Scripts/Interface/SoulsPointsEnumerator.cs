using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class SoulsPointsEnumerator : MonoBehaviour
{
    private Text soulsPoints;
    private Treasury treasury;

    private void Start()
    {
        soulsPoints = GetComponent<Text>();
        treasury = FindObjectOfType<Treasury>();

        treasury.TresureUpdated.AddListener(UpdateSoulsEnumerator);
    }

    private void UpdateSoulsEnumerator()
    {
        soulsPoints.text = treasury.CurrentSoulsPoints.ToString();
    }
}
