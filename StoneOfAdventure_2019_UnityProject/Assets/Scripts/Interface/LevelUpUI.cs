using System.Collections;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.5f;

    private void OnEnable()
    {
        StartCoroutine("DisableTimer");
    }

    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
