using UnityEngine;
using System.Collections;
using StoneOfAdventure.Core;

public class Stunned : MonoBehaviour
{
    [SerializeField] private float timeOfStun = 1f;
    private Unit unit;

    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    public void ApplyStun(float time)
    {
        timeOfStun = time;
        StartCoroutine("StartStunTimer");
    }

    private IEnumerator StartStunTimer ()
    {
        yield return new WaitForSeconds(timeOfStun);
        unit.DisableState();
    }
    public void Cancel()
    {
        StopAllCoroutines();
    }
}
