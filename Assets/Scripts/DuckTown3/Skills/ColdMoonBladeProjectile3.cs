using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class ColdMoonBladeProjectile3 : MonoBehaviour
{
    private float maxDistance;
    private float flySpeed;
    private Vector3 startPos;
    public GameObject hitEffect;

    //for cache 
    private float maxDistanceSqr;

    public void Init(float maxDistance, float flySpeed)
    {
        this.maxDistance = maxDistance;
        this.flySpeed = flySpeed;
        startPos = transform.position;
    }

    private void Start()
    {
        maxDistanceSqr = maxDistance * maxDistance;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * flySpeed * Time.deltaTime);

        float distanceSqr = (transform.position - startPos).sqrMagnitude;
        if (distanceSqr > maxDistanceSqr)
        {
            //Debug.Log("超出最大飞行距离");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttackable enemy = other.gameObject.GetComponent<IAttackable>();
        if (enemy != null)
        {
            enemy.TakeDamage();
        }

        Instantiate(hitEffect, other.ClosestPoint(transform.position), Quaternion.identity);
        Destroy(gameObject);
    }

}
