using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Entity", menuName="ScriptableObjects/EntityInfo")]
public class EntityInfo: ScriptableObject
{
    public List<string> resistance;
}
