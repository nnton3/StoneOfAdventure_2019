using System.Collections;
using UnityEngine;
using StoneOfAdventure.Movement;

public class SoulShard : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private GameObject player;
    private bool moveStarted;
    private Flyer flyer;
    private Treasury treasury;
    private LocationPointsStorage locationPoints;
    [SerializeField] private float impulsePower = 1f;
    [SerializeField] private float movespeed = 3f;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        flyer = GetComponent<Flyer>();
        treasury = FindObjectOfType<Treasury>();
        locationPoints = FindObjectOfType<LocationPointsStorage>();
        col.isTrigger = false;

        AddStartImpulse();
        StartCoroutine("DelayBeforeMove");
    }

    private void AddStartImpulse()
    {
        Vector2 impulseDirection = (new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f))).normalized;
        rb.AddForce(impulseDirection * impulsePower, ForceMode2D.Impulse);
    }

    private void Update()
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
        col.isTrigger = true;
        moveStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && moveStarted) PlayerGetReward();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && moveStarted) PlayerGetReward();
    }

    private void PlayerGetReward()
    {
        treasury.Refill(1);
        locationPoints.AddPoints(1);
        gameObject.SetActive(false);
    }
}
