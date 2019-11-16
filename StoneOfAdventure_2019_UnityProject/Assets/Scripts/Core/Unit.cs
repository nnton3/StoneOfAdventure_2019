using UnityEngine;

namespace StoneOfAdventure.Core
{
    public class Unit : MonoBehaviour
    {
        protected enum State { Idle, MoveHorizontal, MoveVertical, InTheAir, Attack, Skill2, Death,
            Stun
        }
        protected State currentState = State.Idle;

        public virtual void Attack() { return; }
        public virtual void Skill1() { return; }
        public virtual void Skill2() { return; }
        public virtual void MoveHorizontal(float direction) { return; }
        public virtual void MoveVertical(float direction) { return; }
        public virtual void DisableState() { return; }
        public virtual void ApplyStun(float time) { return; }
        public virtual void Dead() { return; }
        public virtual void Jump() { return; }
        public virtual void Fell() { return; }
        public virtual void Born() { return; }
        public virtual void Landed() { return; }
    }
}
