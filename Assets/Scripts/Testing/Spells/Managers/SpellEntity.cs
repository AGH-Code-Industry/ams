using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEntity : MonoBehaviour {
    private Spell _spell;

    private float _createdAt;

    void Start() {
        _createdAt = Time.time;
    }

    void Update() {
        if(_spell is Projectile) {
            UpdateProjectile((Projectile)_spell);
        } else {
            Debug.LogError("Unknown spell type");
        }
    }

    void UpdateProjectile(Projectile projectile) {

        transform.position = transform.position + projectile.speed * Time.deltaTime * transform.forward;

        if(Time.time > _createdAt + projectile.lifeTime) {

            // TODO: Create delegates for destruction

            Destroy(gameObject);
        }
    }

    public void Assign(Spell spell) {
        _spell = spell;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")) {
            bool success = other.TryGetComponent(out Enemy enemy);

            if(success == false) {
                throw new System.Exception("Enemy is missing an Enemy-derived script!");
            }

            if(_spell is Projectile projectile) {
                enemy.DealDamage(projectile.damage, projectile.name);

                Destroy(gameObject);
            } else {
                throw new System.Exception("Unknown spell type!");
            }
        }
    }
}
