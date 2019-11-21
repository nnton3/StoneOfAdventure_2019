using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;

public class MovespeedDebuff : MonoBehaviour
{
    [SerializeField] private int stucsValue;
    [SerializeField] private float slowDownValueAStuc = 0.1f;
    [SerializeField] private float stucLifeTime = 3f;
    private Mover mover;

    private void Start()
    {
        mover = GetComponent<Mover>();
    }

    private void UpdateDebuff()
    {
        mover.ModifyMovespeedScale(stucsValue * slowDownValueAStuc);
    }

    public void SetStucValue(int addedStucNumber)
    {
        stucsValue += addedStucNumber;
    }

    private IEnumerator DebuffLifeTime()
    {
        yield return new WaitForSeconds(stucLifeTime);
        
    }
}
