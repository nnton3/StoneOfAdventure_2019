﻿using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Combat
{
    [RequireComponent(typeof(Flip))]
    [RequireComponent(typeof(Animator))]
    public class Fighter : MonoBehaviour
    {
        #region Variables
        protected Animator anim;
        protected Flip flip;
        [HideInInspector] public UnityEvent Attack;
        private float currentAttackSpeed = 1f;
        public float CurrentAttackSpeed => currentAttackSpeed;

        [SerializeField] protected int baseDamage;
        public int BaseDamage => baseDamage;

        // The impact of the attack modifiers
        public delegate void EffectsOnTarget(GameObject target);
        protected EffectsOnTarget applyEffectsOnTarget;
        public delegate void ModifiersOfDamage(ref int damage);
        protected ModifiersOfDamage applyDamageModifiers;
        #endregion

        protected virtual void Start()
        {
            anim = GetComponent<Animator>();
            flip = GetComponent<Flip>();
        }

        public virtual void StartAttack()
        {
            Attack.Invoke();
            anim.SetTrigger("attack");
        }

        public virtual void CancelAttack()
        {
            anim.SetTrigger("disable");
        }

        public void ModifyAttackSpeed(float addedAttackspeedInPercent)
        {
            currentAttackSpeed += addedAttackspeedInPercent;
            anim.SetFloat("currentAttackspeed", currentAttackSpeed);
        }

        public void AddEffectOfAttack(EffectsOnTarget effect)
        {
            applyEffectsOnTarget += effect;
        }

        public void RemoveEffectOfAttack(EffectsOnTarget effect)
        {
            applyEffectsOnTarget -= effect;
        }

        public void AddModifierOfDamage(ModifiersOfDamage modifier)
        {
            applyDamageModifiers += modifier;
        }

        public void IncreaseBaseDamage(float increaseValue)
        {
            baseDamage += (int)(baseDamage * increaseValue);
        }

        private void OnDisable()
        {
            Attack.RemoveAllListeners();
        }
    }
}
