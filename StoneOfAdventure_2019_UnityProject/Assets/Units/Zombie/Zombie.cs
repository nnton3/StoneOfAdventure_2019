using System;
using UnityEngine;
using StoneOfAdventure.Combat;

public class Zombie : MonoBehaviour
{
    Combat combat;
    EnemyDetector enemyDetector;
    private object target;

    private void Start()
    {
        combat = GetComponent<Combat>();
        enemyDetector = GetComponent<EnemyDetector>();

        enemyDetector.PlayerDetected.AddListener(() => combat.UpdateTarget());
        enemyDetector.PlayerLost.AddListener(() => combat.UpdateTarget());      // TODO add idle state
    }
}
