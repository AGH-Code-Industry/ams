using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Destroyable {
    public class ExplosiveBarrelCollisionTrigger : MonoBehaviour {
        [SerializeField] private ExplosiveBarrel explosiveBarrel;
        
        void OnCollisionEnter(Collision collision) {
            explosiveBarrel.Explode();
        }
    }
}
