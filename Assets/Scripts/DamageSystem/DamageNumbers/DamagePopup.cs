using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace DamageSystem.DamageNumbers
{
    public class DamagePopup : MonoBehaviour
    {
        public static DamagePopup Create(Vector3 position, int damageAmount)
        {
            Transform damagePopupTransform = Instantiate(GameAssets.i.damagePopup, position, Quaternion.identity);
            DamagePopup popup = damagePopupTransform.GetComponent<DamagePopup>();
            popup.Setup(damageAmount);

            return popup;
        }

        private TextMeshPro textMesh;
        private float disappearTimer;
        private Color textColor;

        private Camera mainCam;

        private void Awake()
        {
            textMesh = transform.GetComponent<TextMeshPro>();
            mainCam = Camera.main;
        }

        public void Setup(int damageAmount)
        {
            textMesh.SetText(damageAmount.ToString());
            textColor = textMesh.color;
            disappearTimer = 0.5f;
        }

        private void Update()
        {
            float moveSpeed = 5f;
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;

            disappearTimer -= Time.deltaTime;
            if(disappearTimer < 0)
            {
                float disappearSpeed = 3f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if(textColor.a < 0)
                    Destroy(gameObject);
            }
        }

        private void LateUpdate()
        {
            Vector3 awayDirection = transform.position - mainCam.transform.position;
            Quaternion awayRotation = Quaternion.LookRotation(awayDirection);
            transform.rotation = Quaternion.Euler(awayRotation.eulerAngles.x, 0f, awayRotation.eulerAngles.z);
        }
    }
}
