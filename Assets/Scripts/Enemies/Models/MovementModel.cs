using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Models {
    public abstract class MovementModel : MonoBehaviour {
        public abstract void Move(Vector3 destination);
        public abstract bool IsDestinationReached();
        public abstract bool IsDestinationValid(Vector3 destination);
        public abstract void Stop();

    }
}
