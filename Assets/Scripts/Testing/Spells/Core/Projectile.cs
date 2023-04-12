using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Test/Spell/New Projectile")]
public class Projectile : Spell {
    public GameObject model;
    public float speed;
    public float lifeTime;
    public float damage;
}
