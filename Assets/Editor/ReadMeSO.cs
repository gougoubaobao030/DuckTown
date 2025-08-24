using UnityEngine;


#if UNITY_EDITOR 
[CreateAssetMenu(fileName = "ReadMe", menuName = "Custom/Readme")]
public class ReadMeSO : ScriptableObject
{
    [TextArea(6, 19)]
    public string text;
}
#endif