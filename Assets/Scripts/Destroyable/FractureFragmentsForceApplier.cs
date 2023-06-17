using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Destroyable {
    public class FractureFragmentsForceApplier : MonoBehaviour {
        [SerializeField] private float explosionRadius = 3f;
        [SerializeField] private float explosionForce = 2f;
        [SerializeField] private float explosionUpwardsForceModifier = 1f;
        
        public void Explode() {
            Transform fragmentsContainer = transform.GetChild(transform.childCount - 1);
            Rigidbody[] fragments = fragmentsContainer.gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody fragment in fragments) {
                fragment.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpwardsForceModifier, ForceMode.Impulse);
            }
        }

        private void OnValidate() {
            Debug.Assert(GetComponentInChildren<Fracture>(), "This script must be the parent of the model with the Fracture script");
        }
    }
}
