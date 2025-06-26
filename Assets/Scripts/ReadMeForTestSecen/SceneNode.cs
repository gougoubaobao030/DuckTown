using UnityEngine;

public class SceneNode : MonoBehaviour
{
    public string[] tags;

    //起始三行
    //最多十行
    [TextArea(3, 10)]public string readme;
}
