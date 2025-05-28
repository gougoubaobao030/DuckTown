using UnityEngine;
public enum ItemType {Item, Equipment, Material}

[CreateAssetMenu(fileName = "New Item", menuName = "DuckTown3/Items/Item")]
public class ItemData3 : ScriptableObject
{
    public string ItmeName;
    public Sprite icon;
    public string description;
    public ItemType itemType;
}

