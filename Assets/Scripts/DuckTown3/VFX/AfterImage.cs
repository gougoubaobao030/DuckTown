using UnityEngine;

//闪现出来的残影控制脚本
public class AfterImage : MonoBehaviour
{
    [SerializeField] float liftTime = 0.8f;

    private void Start()
    {
        Destroy(gameObject, liftTime);
    }
}
