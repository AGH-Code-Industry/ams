using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DamageSystem.ReceiveDamage.Elementals {
    public class Damageable : MonoBehaviour, IDamageable {
        public static Damageable instance; // for test
        [SerializeField] private EntitySO entity;
        public UnityEvent Died;
        private readonly List<ExtendedEffect> effects = new();

        private int _currentHealth;

        private void Start() {
            _currentHealth = entity.maxHealth;
            instance = this;
        }

        private void Update() {
        }

        public bool TakeDamage(DamageInfo damageInfo) {
            var damageTaken = 0;
            if (entity.isInvincible) return false;

            foreach (var damageElemental in damageInfo.elementals) damageTaken += damageElemental.elementalDamage;

            _currentHealth = Math.Clamp(_currentHealth - damageTaken, entity.isImmortal ? 1 : 0, entity.maxHealth);

            //Spawn a damage number :)
            Vector3 popupOrigin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            DamageNumbers.DamagePopup.Create(popupOrigin, damageTaken);

            if (_currentHealth == 0) {
                Debug.Log("Dead");
                Died.Invoke();
            };

            TakeEffect(damageInfo);
            return true;
        }

        private void TakeEffect(DamageInfo damageInfo) {
            //Debug.Log("-----------------------");
            //Debug.Log("Effects before:");
            //foreach (var effect in effects) Debug.Log(effect.baseInfo.effectType);

            foreach (var attackElemental in damageInfo.elementals) {
                var effectReaction =
                    new EffectReaction(); // Do przemyślenia co zrobić jak wiele efektów się ze sobą łączy
                var foundEffectIndex = effects.FindIndex(myEffect => {
                    effectReaction = attackElemental.efect.effectReactions.Find(effectReaction =>
                        effectReaction.factor.effectType == myEffect.baseInfo.effectType);
                    return myEffect.baseInfo.effectType == attackElemental.efect.effectType ||
                           !effectReaction.Equals(new EffectReaction());
                });

                ExtendedEffect newEffect;

                if (foundEffectIndex == -1) {
                    newEffect = new ExtendedEffect(attackElemental.efect);
                    effects.Add(newEffect);
                } else if (effects[foundEffectIndex].baseInfo.effectType == attackElemental.efect.effectType) {
                    newEffect = new ExtendedEffect(attackElemental.efect);
                    effects[foundEffectIndex] = newEffect;
                } else {
                    newEffect = new ExtendedEffect(effectReaction.result);
                    effects[foundEffectIndex] = newEffect;
                }

                StartCoroutine(removeEffectByTime(newEffect));
            }

            //Debug.Log("-----------------------");
            //Debug.Log("Effects after:");
            //foreach (var effect in effects) Debug.Log(effect.baseInfo.effectType);
        }


        private IEnumerator removeEffectByTime(ExtendedEffect effect) {
            yield return new WaitForSeconds(effect.baseInfo.duration);
            effects.Remove(effect);
            //Debug.Log("-----------------------");
            //Debug.Log("After remove:");
            //foreach (var effect1 in effects) Debug.Log(effect1.baseInfo.effectType);
        }
    }
}