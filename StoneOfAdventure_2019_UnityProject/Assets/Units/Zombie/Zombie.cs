using System;
using UnityEngine;
using StoneOfAdventure.Combat;

public class Zombie : MonoBehaviour
{
    Fighter combat;
    EnemyDetector enemyDetector;
    private object target;

    private void Start()
    {
        combat = GetComponent<Fighter>();
        enemyDetector = GetComponent<EnemyDetector>();

        enemyDetector.PlayerDetected.AddListener(() => combat.UpdateTarget());
        enemyDetector.PlayerLost.AddListener(() => combat.UpdateTarget());      // TODO add idle state
    }
}
