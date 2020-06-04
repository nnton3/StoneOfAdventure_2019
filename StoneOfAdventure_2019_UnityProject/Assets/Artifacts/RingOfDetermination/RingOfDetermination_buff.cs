using UnityEngine;
using StoneOfAdventure.Combat;
using Zenject;

public class RingOfDetermination_buff : MonoBehaviour
{
    #region Variables
    private Health health;
    private Fighter fighter;

    private int sixtyPercentHeal;
    private int fourtyPercentHeal;
    private int twetyPercentHeal;
    [Inject] private DiContainer Container;
    #endregion

    public void Initialize(int sixtyPercentHeal, int fourtyPercentHeal, int twetyPercentHeal)
    {
        this.sixtyPercentHeal = sixtyPercentHeal;
        this.fourtyPercentHeal = fourtyPercentHeal;
        this.twetyPercentHeal = twetyPercentHeal;

        Container.Inject(this);
    }

    private void Start()
    {
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        fighter.DamageApplied.AddListener(TryToHeal);
    }

    private void TryToHeal()
    {
        var currentHealthInPercent = health.HealthPoints.Value / (health.MaxHealthPoints.Value / 100);

        if (currentHealthInPercent < 60f && currentHealthInPercent >= 40f)
            health.Heal(sixtyPercentHeal);

        if (currentHealthInPercent < 40f && currentHealthInPercent >= 20f)
            health.Heal(fourtyPercentHeal);

        if (currentHealthInPercent < 20f)
            health.Heal(twetyPercentHeal);
    }
}
