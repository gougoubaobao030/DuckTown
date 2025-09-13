using UnityEngine;
using DuckTown3.ObjectPool;
using DuckTown3.SkillSystemV2;


public class ColdMoonBladeProjectile3 : MonoBehaviour
{
    private float maxDistance;
    private float flySpeed;
    private Vector3 startPos;
    public GameObject hitEffect;
    private float damage;

    //for cache 
    private float maxDistanceSqr;

    public void Init(ColdBladeMoonSO data)
    {
        this.maxDistance = data.maxFlyDistance;
        this.flySpeed = data.flySpeed;
        startPos = transform.position;
        damage = data.Damage;
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
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttackable enemy = other.gameObject.GetComponent<IAttackable>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        //Instantiate(hitEffect, other.ClosestPoint(transform.position), Quaternion.identity);
        //objectpool hiteffect
        GameObject hitStar = ObjectPoolManager.Instance.Get(hitEffect);
        hitStar.transform.position = other.ClosestPoint(transform.position);
        hitStar.transform.rotation = Quaternion.identity;

        //Destroy(gameObject);
        gameObject.SetActive(false);

    }

}

