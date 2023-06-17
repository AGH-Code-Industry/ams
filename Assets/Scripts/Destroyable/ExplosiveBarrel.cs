using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Destroyable {
    public class ExplosiveBarrel : MonoBehaviour, IExplosive {
        [SerializeField] private Explosion explosion;
        [SerializeField] private FractureFragmentsForceApplier model;
        [SerializeField] private float destroyAfterExplosionDelay = 5f;
        private Collider collider;

        void Awake() {
            collider = GetComponent<Collider>();
        }

        public void Explode() {
            collider.enabled = false;
            explosion.gameObject.SetActive(true);
            model.Explode();
            StartCoroutine(DestroyAfterDelay());
        }

        IEnumerator DestroyAfterDelay() {
            yield return new WaitForSeconds(destroyAfterExplosionDelay);
            Destroy(gameObject);
        }
    }
}
