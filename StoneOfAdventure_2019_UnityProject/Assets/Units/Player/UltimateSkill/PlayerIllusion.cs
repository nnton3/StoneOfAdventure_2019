using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class PlayerIllusion : MonoBehaviour
{
    [SerializeField] private Vector3 applicationArea;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float lifeTime;

    private Flip flip;
    private Fighter fighter;
    private Animator animator;
    private IllusionStates illusionState = IllusionStates.Idle;

    enum IllusionStates { Idle, Attack }

    private void Start()
    {
        flip = GetComponent<Flip>();
        fighter = GetComponent<Fighter>();
        animator = GetComponent<Animator>();

        StartCoroutine("LifeTimer");
    }

    private void FixedUpdate()
    {
        if (illusionState == IllusionStates.Idle) TryToAttackEnemie();
    }

    private void TryToAttackEnemie()
    {
        Collider2D enemieInAttackArea = Physics2D.OverlapBox(
            transform.position,
            applicationArea,
            0f,
            layerMask);
        if (enemieInAttackArea == null) return;
        var positionDifference = Mathf.Sign(enemieInAttackArea.transform.position.x - transform.position.x);
        if (!flip.isFacingRight && positionDifference == 1f ||
            flip.isFacingRight && positionDifference == -1f) flip.FlipObject();
        fighter.StartAttack();
        illusionState = IllusionStates.Attack;
    }

    public void IdleState()
    {
        illusionState = IllusionStates.Idle;
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        animator.SetTrigger("die");
    }

    public void DestroyIllusion()
    {
        Destroy(gameObject);
    }
}
