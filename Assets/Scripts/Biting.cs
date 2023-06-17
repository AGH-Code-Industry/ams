using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections.Generic;
using UnityEngine;

public class Biting : MonoBehaviour {
    private Renderer objectRenderer;
    private Transform player;
    float biteCounter = 0;
    Damageable playerDamagebale;
    [SerializeField] private List<AttackElemental> elemenatlsss;


    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player").transform;
        playerDamagebale = player.GetComponent<Damageable>();

        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(transform.position, player.position);


        if (distance <= 1.5f) {
            biteCounter += 1;
            if (biteCounter % 100 == 0) {
                objectRenderer.material.color = Color.black;
                DamageInfo info = new DamageInfo(elemenatlsss, gameObject);
                playerDamagebale.TakeDamage(info);


            } else {
                objectRenderer.material.color = Color.red;
            }
        } else {
            objectRenderer.material.color = Color.red;

        }

    }
}
