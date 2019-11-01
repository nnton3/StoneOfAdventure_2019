using UnityEngine;

namespace StoneOfAdventure.Core
{
    public class Unit : MonoBehaviour
    {
        public BaseState State;

        public virtual void DisableState() { return; }
        public virtual void ApplyStun(float time) { return; }
        public virtual void Dead() { return; }

        public virtual void Fell() { return; }
    }
}
