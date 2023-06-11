using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Destroyable {
    public class ExplosiveBarrel : MonoBehaviour, IExplosive {
        [SerializeField] private ExplosiveBarrelCollisionTrigger collisionTrigger;
        [SerializeField] private GameObject explosion;

        public void Explode() {
            explosion.SetActive(true);
            collisionTrigger.gameObject.SetActive(false);
        }
    }
}
