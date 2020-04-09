using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using UnityEngine;

public class BatStateController : UnitContainsAward
{
    private GameObject player;
    private Flyer flyer;
    private Animator anim;

    [SerializeField] private float movespeed;
    
    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
        flyer = GetComponent<Flyer>();
        anim = GetComponent<Animator>();

        transform.position = transform.position + Vector3.left * 20f;
    }

    private void FixedUpdate()
    {
        if (player) Move((player.transform.position - transform.position).normalized);
    }

    private void Move(Vector2 direction)
    {
        flyer.FlyTo(direction, movespeed);
    }

    public override void Dead()
    {
        player = null;
        Move(Vector2.zero);
        anim.SetTrigger("dead");
        CreateReward();
    }

    public void DestroyUnit()
    {
        Destroy(gameObject);
    }
}
