using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoneOfAdventure.Movement;
using UnityEngine.UI;

public class SoulShard : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
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
        player = GameObject.FindGameObjectWithTag("Player");
        flyer = GetComponent<Flyer>();
        treasury = GameObject.FindObjectOfType<Treasury>();
        locationPoints = FindObjectOfType<LocationPointsStorage>();

        Vector2 impulseDirection = (new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(0f, 1f))).normalized;
        rb.AddForce(impulseDirection * impulsePower, ForceMode2D.Impulse);
        StartCoroutine("DelayBeforeMove");
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
        moveStarted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) PlayerGetReward();
    }

    private void PlayerGetReward()
    {
        treasury.Refill(1);
        locationPoints.AddPoints(1);
        Destroy(gameObject);
    }
}
