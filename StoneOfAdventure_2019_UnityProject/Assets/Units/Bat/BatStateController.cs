using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using UnityEngine;

public class BatStateController : Unit
{
    private GameObject player;
    private Flyer flyer;

    [SerializeField] private float movespeed;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flyer = GetComponent<Flyer>();
    }

    private void Update()
    {
        if (player)
        {
            Move();
        }
    }

    private void Move()
    {
        flyer.FlyTo((player.transform.position - transform.position).normalized, movespeed);
    }
}
