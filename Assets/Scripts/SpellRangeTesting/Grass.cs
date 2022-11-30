using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
   [SerializeField] private Material _grassMaterial;
   [SerializeField] private Material _grassEnteredMaterial;
   [SerializeField] private MeshRenderer _meshRenderer;

   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.tag == "Bullet")
      {
         Debug.Log("Entered");
         _meshRenderer.material = _grassEnteredMaterial;
      }
   }

   private void OnCollisionExit(Collision other)
   {
      if (other.gameObject.tag == "Bullet")
      {
         Debug.Log("Left");
         _meshRenderer.material = _grassMaterial;
      }
   }
}
