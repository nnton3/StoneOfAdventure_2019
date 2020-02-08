using UnityEngine;
using StoneOfAdventure.Combat;

public class RingOfDetermination_buff : MonoBehaviour
{
    #region Variables
    private Health health;
    private Fighter fighter;

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
        fighter = GetComponent<Fighter>();
    }
}
