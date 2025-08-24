using UnityEngine;

namespace DuckTown3.SkillSystemV1
{
    public class MushroomProjectileSkillTest3 : MonoBehaviour
    {
        private Transform shootPointer;
        [SerializeField] private Vector3 shootOffset = new Vector3(0, 0, 0);

        [SerializeField] private GameObject mushroomProjectilePrefab;
        //可惜放的不是这里
        //[SerializeField] private GameObject mushroomExplosionPrefab;

        //throw mushroom
        [SerializeField] private float YForce = 1.0f;
        [SerializeField] private float mushroomSpeed = 15.0f;

        //counter for explosion
        //可惜也不是这里
        //[SerializeField] private float explosionTimer = 2.0f;

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
}