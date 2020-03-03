using UnityEngine;
using StoneOfAdventure.Combat;

public class BrokenClock_buff : MonoBehaviour
{
    private float chance;
    private float reduceValue;
    private SkillBase[] skills;

    public void Initialize(float chance, float reduceValue)
    {
        this.chance = chance;
        this.reduceValue = reduceValue;
    }

    private void Start()
    {
        skills = GetComponents<SkillBase>();

        for(int i = 0; i < skills.Length; i++)
        {
            skills[i].SkillUsed.AddListener(TryToResuceCooldown);
        }
    }

    private void TryToResuceCooldown()
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
