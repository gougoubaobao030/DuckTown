using UnityEngine;

//===========================================================
//Func: SetIdle used for nextstate
//not juet for patrol
//===========================================================

namespace DuckTown3
{
    public class EnemyIdleState : EnemyGroundState
    {
        //private EnemyController enemy;
        //private Animator animator;
        //private EnemyStateMachine fsm;

        //wait to start patrol
        private float restTime;
        private float timer = 0.0f;
        private IEnemyState nextState;

        public EnemyIdleState(EnemyController enmey, Animator animator, EnemyStateMachine fsm)
            : base(enmey, animator, fsm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            animator.Play("BlueDuckIdle");
            timer = restTime;
        }

        public override void Exit()
        {
            base.Exit();
            timer = 0.0f;

        }

        public override void Update()
        {
            base.Update();
            if (timer <= 0.0f)
            {
                fsm.ChangeState(nextState);
            }
            timer -= Time.deltaTime;

        }

        //在制作等待几秒进入idle问题上，不用协程
        //为什么呢：
        //不会自动停，突然凉掉的空引用，多次调用
        public void SetIdle(float rest, IEnemyState next)
        {
            restTime = rest;
            nextState = next;
        }
    }
}
