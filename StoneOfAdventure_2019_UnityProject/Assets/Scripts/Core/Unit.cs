using UnityEngine;

namespace StoneOfAdventure.Core
{
public class Unit : MonoBehaviour
{
    public virtual void DisableState() { return; }
    public virtual void ApplyStun(float time) { return; }
}
}
