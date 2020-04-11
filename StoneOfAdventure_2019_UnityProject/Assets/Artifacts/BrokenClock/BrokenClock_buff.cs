using UnityEngine;
using StoneOfAdventure.Combat;
using Zenject;

public class BrokenClock_buff : MonoBehaviour
{
    private float chance;
    private float reduceValue;
    private SkillBase[] skills;

    [Inject] private DiContainer Container;
    [Inject (Id = "Player")] private Animator anim;
    [Inject] private SignalBus signalBus;

    public void Initialize(float chance, float reduceValue)
    {
        this.chance = chance;
        this.reduceValue = reduceValue;

        Container.Inject(this);
    }

    private void Start()
    {
        skills = GetComponents<SkillBase>();

        for(int i = 0; i < skills.Length; i++)
        {
            skills[i].SkillUsed.AddListener(TryToReduceCooldown);
        }
    }

    private void TryToReduceCooldown()
    {
        var currentChance = Random.Range(0f, 1f);
        if (currentChance <= chance)
        {
            signalBus.Fire(new BrokenClockTriggered { reducedTime = reduceValue });
            anim.SetTrigger("brokenClockTriggered");
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].ReduceCoolDown(reduceValue);
            }
        }
    }
}
