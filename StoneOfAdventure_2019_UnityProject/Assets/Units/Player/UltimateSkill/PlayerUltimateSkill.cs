using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class PlayerUltimateSkill : SkillBase
{
    #region Variables
    [SerializeField] private GameObject playerIllusionPref;
    private GroundTileFinder tileFinder; 
    private readonly int IllusionsNumber = 2;
    [SerializeField] private float positionIsertRelativePlayer = 2f;
    private Animator anim;
    #endregion

    private void Start()
    {
        tileFinder = GetComponent<GroundTileFinder>();
        anim = GetComponent<Animator>();
    }

    public override void StartUse()
    {
        base.StartUse();
        anim.SetTrigger("ultimateSkill");
    }

    private void InstanceIllusion(Vector3 position)
    {
        for (int i = 0; i < IllusionsNumber; i++)
        {
            if (position == Vector3.zero) return;
            Instantiate(playerIllusionPref, position, Quaternion.identity);
        }
    }

    public void CreateIllusion()
    {
        var validTile = tileFinder.PositionIsValid(transform.position + Vector3.right * positionIsertRelativePlayer);
        InstanceIllusion(validTile);
        validTile = tileFinder.PositionIsValid(transform.position - Vector3.right * positionIsertRelativePlayer);
        InstanceIllusion(validTile);
    }
}
