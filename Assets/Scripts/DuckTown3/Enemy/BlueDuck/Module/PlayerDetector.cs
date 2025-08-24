using DuckTown3;
using UnityEngine;

public class PlayerDetector
{
    private Transform enemy;
    private Transform duck;
    private EnemyRuntimeData data;

    public PlayerDetector(Transform enemy, Transform duck, EnemyRuntimeData data)
    { 
        this.enemy = enemy;
        this.duck = duck;
        this.data = data;
    }

    public bool IsPlayerInRange()
    {
        //step1. calc dir
        Vector3 dir = (duck.position - enemy.position).normalized;
        //step2. calc angle from foward to dir
        float angle = Vector3.Angle(enemy.forward, dir);
        //step3. calc distance from enmey to duck
        float distance = Vector3.Distance(duck.position, enemy.position);

        bool isInAngle = angle <= data.viewAngle / 2;
        bool isInDistance = distance <= data.viewRadius;

        return isInAngle && isInDistance;
    }
}
