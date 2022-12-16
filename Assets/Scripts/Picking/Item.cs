using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
    
}
