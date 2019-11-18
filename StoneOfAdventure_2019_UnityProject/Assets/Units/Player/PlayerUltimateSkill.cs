using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class PlayerUltimateSkill : SkillBase
{
    #region Variables
    
    private GameObject playerIllusionPref;
    #endregion

    public override void StartUse()
    {
        base.StartUse();
        InstanceIllusions();
    }

    private void InstanceIllusions()
    {
        
    }
}
