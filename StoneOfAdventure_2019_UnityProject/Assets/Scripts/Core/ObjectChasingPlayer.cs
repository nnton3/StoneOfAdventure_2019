using System.Collections;
using UnityEngine;
using StoneOfAdventure.Movement;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class ObjectChasingPlayer : MonoBehaviour
{
    #region Variables
    [Inject(Id = "Player")] protected PlayerStateController player;
    [Inject] private DiContainer container;

    private Rigidbody2D rb;
    private bool moveStarted;
    private Flyer flyer;

    [SerializeField] private float impulsePower = 1f;
    [SerializeField] private float movespeed = 3f;
    #endregion

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        flyer = GetComponent<Flyer>();
        container.Inject(this);
    }

    private void Start()
    {
        this.OnTriggerStay2DAsObservable()
            .Subscribe(collision =>
            {
                if (collision.CompareTag("Player") && moveStarted)
                {
                    PlayerGetObject();
                }
            });
    }

    private void OnEnable()
    {
        moveStarted = false;
        AddStartImpulse();
        StartCoroutine("DelayBeforeMove");
    }

    private void AddStartImpulse()
    {
        Vector2 impulseDirection = (new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f))).normalized;
        rb.AddForce(impulseDirection * impulsePower, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (moveStarted)
        {
            Move();
        }
    }

    private void Move()
    {
        flyer.FlyTo((player.transform.position - transform.position + new Vector3(0f, 0.5f, 0f)).normalized, movespeed);
    }

    IEnumerator DelayBeforeMove()
    {
        yield return new WaitForSeconds(0.5f);
        moveStarted = true;
    }
    
    protected virtual void PlayerGetObject()
    {
        gameObject.SetActive(false);
    }
}
