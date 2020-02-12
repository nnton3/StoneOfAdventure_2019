using System.Collections;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    [SerializeField] private float eventTimer;

    private void Start()
    {
        if (timeToDestroy != 0f)
            StartTimer();
        else
            timeToDestroy = eventTimer;
    }

    public void StartTimer()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
