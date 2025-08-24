using UnityEngine;

namespace DuckTown3
{
    //这里暂时可以理解为
    //由于用了泛型EnemyDuckStateBase这个类就不存在了
    //现在存在的是EnemyDuckStateBase<T>型
    public abstract class EnemyDuckStateBase<T> : IEnemyState where T : EnemyDuckStateBase<T>
    {
        protected EnemyController enemy;
        protected Animator animator;
        protected EnemyStateMachine fsm;

        protected EnemyDuckStateBase(EnemyController enmey, Animator animator, EnemyStateMachine fsm)
        {
            this.enemy = enmey;
            this.animator = animator;
            this.fsm = fsm;
        }

        public virtual void Enter()
        {
            //Debug.Log($"[EnemyState] Enter -- {this.GetType()} ");
            SetStateName();
        }

        public virtual void Exit()
        {
            //Debug.Log($"[EnemyState] Exit -- {typeof(T)} ");
            //Debug.Log($"[EnemyState] Enter -- {this.GetType()} ");
        }

        public virtual void Update()
        {
            
        }

        protected void SetStateName()
        { 
            enemy.StateName = this.GetType().Name;
        }
    }
}
