using UnityEngine;

public class AfterImage : MonoBehaviour
{
    [SerializeField] float liftTime = 0.8f;

    private void Start()
    {
        Destroy(gameObject, liftTime);
    }
}
