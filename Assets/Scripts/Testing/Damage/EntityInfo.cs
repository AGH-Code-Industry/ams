using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Entity", menuName="ScriptableObjects/EntityInfo")]
public class EntityInfo : ScriptableObject {
    public bool isInvincible;
    public bool isImmortal;
    public int maxHealth;
}
