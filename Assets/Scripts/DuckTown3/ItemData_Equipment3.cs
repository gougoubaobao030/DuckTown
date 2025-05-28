using UnityEngine;


public enum EquipmentType
{
    Amulet,
    Weapon,
    Armor,
    Flask
}

[CreateAssetMenu(fileName = "New Item", menuName = "DuckTown3/Items/Equipment")]
public class ItemData_Equipment3 : ItemData3
{
    public EquipmentType EquipmentType;
}
