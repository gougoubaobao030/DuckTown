using UnityEngine;

public class Duck3 : MonoBehaviour
{
    public static Duck3 instance;

    //movement
    //interactor
    //attack
    //要调用的接口
    [Header("Skill Spawn Point")]
    public Transform SlashPointer;

    private void Awake()
    {
        if (instance != null && instance != this)
        { 
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }
}
