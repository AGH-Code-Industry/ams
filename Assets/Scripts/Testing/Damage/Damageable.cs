using System;
using System.Collections;
using System.Collections.Generic;
using Testing.Damage;
using UnityEngine;
using UnityEngine.Events;

namespace Testing.Damage
{
    public class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] private EntityInfo entity;
        public UnityEvent Died;

        private int _currentHealth;
        private readonly List<ExtendedEffect> effects = new List<ExtendedEffect>();

        public static Damageable instance; // for test

        private void Start()
        {
            _currentHealth = entity.maxHealth;
            instance = this;
        }

        public bool TakeDamage(DamageInfo damageInfo)
        {
            int damageTaken = 0;
            if (entity.isInvincible) return false;

            foreach (var damageElemental in damageInfo.elementals)
            {
                damageTaken += damageElemental.elementalDamage;
            }

            _currentHealth = Math.Clamp(_currentHealth - damageTaken, entity.isImmortal ? 1 : 0, entity.maxHealth);

            if (_currentHealth == 0)
            {
                Died.Invoke();
            }
            
            TakeEffect(damageInfo);
            return true;
        }

        private void TakeEffect(DamageInfo damageInfo) {
            Debug.Log("-----------------------");
            Debug.Log("Effects before:");
            foreach (var effect in effects) {
                Debug.Log(effect.baseInfo.effectType);   
            }
            
            foreach (var attackElemental in damageInfo.elementals) {
                var effectReaction = new EffectReaction(); // Do przemyślenia co zrobić jak wiele efektów się ze sobą łączy
                var foundEffectIndex = effects.FindIndex(myEffect => {
                    effectReaction = attackElemental.efect.effectReactions.Find(effectReaction => effectReaction.factor.effectType == myEffect.baseInfo.effectType);
                    return myEffect.baseInfo.effectType == attackElemental.efect.effectType || !effectReaction.Equals(new EffectReaction());
                });

                if (foundEffectIndex == -1) {
                    effects.Add(new ExtendedEffect(attackElemental.efect));
                }
                else if (effects[foundEffectIndex].baseInfo.effectType == attackElemental.efect.effectType) {
                    effects[foundEffectIndex] = new ExtendedEffect(attackElemental.efect);
                }
                else {
                    effects[foundEffectIndex] = new ExtendedEffect(effectReaction.result);
                }
            }

            Debug.Log("-----------------------");
            Debug.Log("Effects after:");
            foreach (var effect in effects) {
                Debug.Log(effect.baseInfo.effectType);   
            }
        }
    }
}