using System;
using StoneOfAdventure.Combat;
using UnityEngine;
using Zenject;

public class PlayerAudioController : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip steps;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip brockenClockTriggered;

    [Inject(Id = "Player")] private Fighter fighter; 
    [Inject(Id = "Player")] private AudioSource audioSource;
    [Inject] SignalBus signalBus;
    #endregion

    private void Start()
    {
        signalBus.Subscribe<PlayerStartWalk>(PlaySteps);
        signalBus.Subscribe<PlayerStopWalk>(StopSteps);
        signalBus.Subscribe<BrokenClockTriggered>(PlaySkillsCoolDownReduced);
        fighter.UseAttack.AddListener(PlayAttackEffect);
        fighter.DamageApplied.AddListener(PlayHitEffect);
    }

    private void PlayAttackEffect()
    {
        audioSource.PlayOneShot(attack);
    }

    private void PlayHitEffect()
    {
        audioSource.PlayOneShot(hit);
    }

    private void PlaySteps()
    {
        audioSource.loop = true;
        audioSource.clip = steps;
        audioSource.Play();
    }

    private void StopSteps()
    {
        audioSource.Stop();
        audioSource.loop = false;
    }

    private void PlaySkillsCoolDownReduced()
    {
        audioSource.PlayOneShot(brockenClockTriggered, 0.5f);
    }
}
