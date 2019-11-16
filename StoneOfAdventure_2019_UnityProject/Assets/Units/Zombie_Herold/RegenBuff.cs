using UnityEngine;
using System.Collections.Generic;
using StoneOfAdventure.Combat;

public class RegenBuff : MonoBehaviour
{
    [SerializeField] private float healValue = 1f;
    [SerializeField] private float periodicity = 1f;

    private void Start()
    {
        InvokeRepeating("Heal", 0f, periodicity);
    }

    private void Heal()
    {
        var healths = Physics2D.OverlapBoxAll(
                        transform.position + applicationAreaCenter,
                        applicationArea,
                        0f,
                        layerMask);
        foreach(var health in healths)
        {
            health.GetComponent<Health>().Heal(healValue);
        }
    }

    [SerializeField] private Vector3 applicationAreaCenter;
    [SerializeField] private Vector3 applicationArea;
    [SerializeField] private bool applicationAreaVisible;
    [SerializeField] private LayerMask layerMask;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (applicationAreaVisible)
            Gizmos.DrawWireCube(transform.position + applicationAreaCenter, applicationArea);
    }
}
