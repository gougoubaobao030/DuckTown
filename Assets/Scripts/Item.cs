using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item: ScriptableObject
{
    [Header("Both")]
    public Sprite image;

    [Header("Only Ui是否可以叠加")]
    public bool stackable = true;

}
