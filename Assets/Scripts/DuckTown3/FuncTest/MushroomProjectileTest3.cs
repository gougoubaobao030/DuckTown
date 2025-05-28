using UnityEngine;

public class MushroomProjectileTest3 : MonoBehaviour
{
    private Transform shootPointer;
    [SerializeField] private Vector3 shootOffset = new Vector3(0, 0, 0);

    [SerializeField] private GameObject mushroomProjectilePrefab;
    [SerializeField] private float YForce = 1.0f;
    [SerializeField] private float mushroomSpeed = 15.0f;

    private void Awake()
    {

    }

    private void Start()
    {
        shootPointer = Duck3.instance.SlashPointer;

    }

    private void Update()
    {
        var shootPos = shootPointer.TransformPoint(shootOffset);
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            GameObject mushroom = Instantiate(mushroomProjectilePrefab, shootPos, shootPointer.rotation);

            Rigidbody rb = mushroom.GetComponent<Rigidbody>();

            Vector3 throwDierection = shootPointer.forward * 1.0f + shootPointer.up * YForce;
            rb.linearVelocity = throwDierection.normalized * mushroomSpeed;
        }
    }
}
