using UnityEngine;

public class SlashEffect3 : MonoBehaviour
{
    public float speed = 180f;       // 飞行速度
    public float lifeTime = 2.0f;  // 存活时间

    private float timer = 0f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
