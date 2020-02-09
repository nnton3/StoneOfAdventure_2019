using UnityEngine;
using StoneOfAdventure.Combat;

public class RingOfDetermination_buff : MonoBehaviour
{
    #region Variables
    private Health health;
    private PlayerFighter fighter;

    private int sixtyPercentHeal;
    private int fourtyPercentHeal;
    private int twetyPercentHeal;
    #endregion

    public void Initialize(int sixtyPercentHeal, int fourtyPercentHeal, int twetyPercentHeal)
    {
        this.sixtyPercentHeal = sixtyPercentHeal;
        this.fourtyPercentHeal = fourtyPercentHeal;
        this.twetyPercentHeal = twetyPercentHeal;
    }

    private void Start()
    {
        health = GetComponent<Health>();
        fighter = GetComponent<PlayerFighter>();
        fighter.ApplyDamage.AddListener(TryToHeal);
    }

    private void TryToHeal()
    {
        var currentHealthInPercent = health.HealthPoints / (health.MaxHealthPoints / 100);
        Debug.Log($"currentHealthInPercent = {currentHealthInPercent}");
        if (currentHealthInPercent < 60f && currentHealthInPercent >= 40f)
            health.Heal(sixtyPercentHeal);

        if (currentHealthInPercent < 40f && currentHealthInPercent >= 20f)
            health.Heal(fourtyPercentHeal);

        if (currentHealthInPercent < 20f)
            health.Heal(twetyPercentHeal);
    }
}
