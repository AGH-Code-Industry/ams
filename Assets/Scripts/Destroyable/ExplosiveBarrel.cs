using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Destroyable {
    public class ExplosiveBarrel : MonoBehaviour, IExplosive {
        [SerializeField] private Explosion explosion;
        [SerializeField] private FractureFragmentsForceApplier model;
        [SerializeField] private float destroyAfterExplosionDelay = 5f;

        public void Explode() {
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
