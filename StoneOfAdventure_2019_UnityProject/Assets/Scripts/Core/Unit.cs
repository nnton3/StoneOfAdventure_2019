using UnityEngine;

namespace StoneOfAdventure.Core
{
    public class Unit : MonoBehaviour
    {
        public BaseState _State;

        public virtual void Attack() { return; }
        public virtual void DisableState() { return; }
        public virtual void ApplyStun(float time) { return; }
        public virtual void Dead() { return; }
        public virtual void Fell() { return; }
        public virtual void Born() { return; }
        public virtual void Landed() { return; }
    }
}
