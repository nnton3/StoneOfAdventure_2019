using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class IronPlate_Artifact : Artifact
    {
        [SerializeField] private float damageResistance = 2f;

        public override void AddEffect()
        {
            var damageResist = player.AddComponent<DamageResistance>();
            damageResist.Initialize(damageResistance);
        }
    }
}
