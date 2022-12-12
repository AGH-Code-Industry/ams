using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
   [SerializeField] private Material _grassMaterial;
   [SerializeField] private Material _grassEnteredMaterial;
   [SerializeField] private MeshRenderer _meshRenderer;

   private double _lastTime;

   private void Start() {
      _lastTime = Time.time;
   }

   private void Update() {
      if (Time.time - _lastTime > Time.deltaTime * 20) {
          _meshRenderer.material = _grassMaterial;
      }
   }

   private void OnCollisionEnter(Collision other) {
      if (other.gameObject.CompareTag("Bullet")) {
         _lastTime = Time.time;
         _meshRenderer.material = _grassEnteredMaterial;
      }
   }

   private void OnCollisionStay(Collision other) {
      if (other.gameObject.CompareTag("Bullet")) {
         _lastTime = Time.time;
      }
   }
}
