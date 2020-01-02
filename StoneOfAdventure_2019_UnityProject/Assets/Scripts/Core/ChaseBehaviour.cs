using UnityEngine;
using System.Collections;
using StoneOfAdventure.Core;

public class ChaseBehaviour : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;

    protected Unit unit;
    private Flip flip;
    private GroundTileScanner tileScanner;
    protected GameObject player;

    protected virtual void Start()
    {
        unit = GetComponent<Unit>();
        flip = GetComponent<Flip>();
        tileScanner = GetComponent<GroundTileScanner>();
        player = FindObjectOfType<PlayerStateController>().gameObject;
    }

    public virtual void UpdateChaseBehaviour()
    {
        if (PlayerInAttackRange())
        {
            if (PlayerInFront()) unit.Attack();
        }
    }

    protected bool PlayerInFront()
    {
        if (flip.isFacingRight && CalculateDirection() == 1f ||
            !flip.isFacingRight && CalculateDirection() == -1f)
            return true;
        else return false;
    }

    public float CalculateDirection()
    {
        if (!tileScanner.UnitOnTheGround()) return 0f;
        return Mathf.Sign(player.transform.position.x - transform.position.x);
    }

    private bool PlayerInAttackRange()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) <= attackRange;
    }

}
