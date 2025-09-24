using UnityEngine;

namespace DuckTown3
{
    public class EnemyMover
    {
        private EnemyController enemy;
        //弥补一个知识，这里的transform是实时更新的
        //知道为什么吗...
        //因为传的是指针...
        private Transform transform;
        private CharacterController cc;
        //private float moveSpeed;

        private float ySpeed = 0.0f;
        //private float gravity = -8.0f;

        public EnemyMover(EnemyController enemy)
        {
            this.enemy = enemy;
            transform = enemy.transform;
            cc = enemy.CC;
            //this.moveSpeed = moveSpeed;
        }

        public void MoveToward(Vector3 target, float speed)
        {
            //step1. calculate direction
            Vector3 fullDir = target - transform.position;
            //dir.y = 0;
            //dir = dir.normalized;

            // 1. 水平方向
            Vector3 horizontalDir = fullDir;
            horizontalDir.y = 0;
            horizontalDir = horizontalDir.normalized;

            if (cc.isGrounded)
            {
                if (ySpeed < 0)
                {
                    ySpeed = -0.5f;
                }               
            }
            else
            {
                ySpeed += enemy.RuntimeData.gravity * Time.deltaTime;
            }

            // 3. 融合坡度方向
            float slopeAssist = fullDir.y * speed;
            float verticalMotion = ySpeed + slopeAssist;

            //Vector3 velocty = dir * speed;
            //velocty.y = ySpeed;

            Vector3 motion = horizontalDir * speed;
            motion.y = verticalMotion;

            //step2. change position by movespeed
            //transform.position += dir * speed * Time.deltaTime;
            //Vector3 motion = velocty * Time.deltaTime;
            Vector3 velocity = motion * Time.deltaTime;

            if (motion.sqrMagnitude > 0.0001f)
            {
                cc.Move(velocity);
            }
            //step3. face foward
            //if (dir != Vector3.zero)
            //{
            //transform.forward = dir;
            //}

            //transform.LookAt(target);
            //dir.y = 0f;
            Vector3 desired = enemy.Agent.desiredVelocity;
            //if (horizontalDir != Vector3.zero)
            //{
            //    这个旋转它可能，对墙旋转....
            //    Quaternion rotate = Quaternion.LookRotation(horizontalDir);
            //    transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 10f * Time.deltaTime);
            //}
            if (desired.sqrMagnitude > 0.001f)
            {
                Quaternion rotate = Quaternion.LookRotation(desired.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 10f * Time.deltaTime);
            }

            //哦，就是让agent觉得已经移动到它认为的位置了？然后它就可以放心找下一个位置了？
            //不然觉得还没有移动到，于是一直在调整
            enemy.Agent.nextPosition = enemy.transform.position;
        }

        public void RotateToTarget(Vector3 targetPos, float rotateSpeed)
        {
            Vector3 dir = (targetPos - transform.position).normalized;
            dir.y = 0;

            Quaternion rotate = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, rotateSpeed * Time.deltaTime);
        }

        public bool IsAt(Vector3 target, float threshold)
        {
            Vector3 horizontalSelf = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 horizontalTarget = new Vector3(target.x, 0, target.z);
            return Vector3.Distance(horizontalSelf, horizontalTarget) < threshold;
            //bool isAt = Vector3.Distance(transform.position, target) < threshold;
            //return isAt;
        }
    }
}
