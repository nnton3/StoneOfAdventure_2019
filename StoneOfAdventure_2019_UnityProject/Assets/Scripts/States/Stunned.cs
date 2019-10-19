using UnityEngine;
using System.Collections;

public class Stunned : MonoBehaviour
{
    [SerializeField] private float timeOfStun = 1f;
    private ZombieStateController unit;

    private void Start()
    {
        unit = GetComponent<ZombieStateController>();
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
