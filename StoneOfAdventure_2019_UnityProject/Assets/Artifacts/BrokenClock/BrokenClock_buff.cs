using UnityEngine;
using StoneOfAdventure.Combat;
using Zenject;

public class BrokenClock_buff : MonoBehaviour
{
    private float chance;
    private float reduceValue;
    private SkillBase[] skills;
    [Inject] DiContainer Container;

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
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].ReduceCoolDown(reduceValue);
            }
        }
    }
}
