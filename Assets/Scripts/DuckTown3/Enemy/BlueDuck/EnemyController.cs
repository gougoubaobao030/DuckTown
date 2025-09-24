using DuckTown3.Enemy;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DuckTown3.ObjectPool;
using DuckTown3.UI;
using UnityEngine.AI;

namespace DuckTown3
{
    public class EnemyController : MonoBehaviour, IAttackable
    {
        [SerializeField] private Transform yellowDuck;
        public Transform YellowDuck => yellowDuck;
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterController cc;
        public CharacterController CC => cc;
        [SerializeField] private NavMeshAgent agent;
        public NavMeshAgent Agent => agent;

        [SerializeField] private EnemyDuckConfig configSO;
        public EnemyDuckConfig Config => configSO;
        private
            EnemyRuntimeData runtimeData;
        public EnemyRuntimeData RuntimeData => runtimeData;

        private EnemyStateMachine stateMachine;

        [Header("Enmey Data")]
        //[field: SerializeField] public float moveSpeed { get; private set; } = 3.0f;

        [Header("Patrol Data")]
        public Transform[] patrolPoints;
        [field: SerializeField] public float threshold { get; private set; } = 0.2f;

        [Header("State")]
        public EnemyIdleState IdleState;
        public EnemyPatrolState PatrolState { get; private set; }
        public EnemyChaseState ChaseState { get; private set; }
        public EnemyAttackState AttackState { get; private set; }
        public EnemyCoolDownState CoolDownState { get; private set; }
        public EnemyComboState ComboState { get; private set; }

        [Header("Module")]
        //public EnemyMover EnemyMover { get; private set; }
        //总归 分成私有字段和公共只读属性 比较架构清晰，比较装逼
        //听说方便debug
        private EnemyMover mover;
        public EnemyMover Mover => mover;

        private PlayerDetector detector;
        public PlayerDetector Detector => detector;

        [Header("Idle To Patorl To Idle")]
        [field: SerializeField] public float restTime { get; private set; } = 4.0f;

        [Header("State Debug")]
        public string StateName;

        [Header("Skill Manager")]
        private EnemySkillManager skillManager;
        public EnemySkillManager SkillManager => skillManager;
        [SerializeField]private EnemySkillExecutor skillExecutor;
        public EnemySkillExecutor SkillExecutor => skillExecutor;

        [SerializeField]private List<BlueDuckSkillSO> skillSOs;

        [SerializeField] private Transform skillPointer;
        public Transform SkillPointer => skillPointer;

        //healthbar
        private GameObject healthBar;

        //Tmp will do it into deadstate later
        [SerializeField] private Transform returnPointer;
        [SerializeField] private GameObject vanishEffect;
        private void Awake()
        {
            //在awake之前，inspector拖入的东东已经生成好了
            //但是awake并没有调用

            //agent
            agent.updatePosition = false;
            agent.updateRotation = false;

            //module
            runtimeData = new EnemyRuntimeData(configSO);
            mover = new EnemyMover(this);
            detector = new PlayerDetector(this.transform, yellowDuck, runtimeData);

            //skill
            skillManager = new EnemySkillManager(skillSOs);
            skillExecutor.Init(this, animator);

            //statemachine
            stateMachine = new EnemyStateMachine();

            IdleState = new EnemyIdleState(this, animator, stateMachine);
            PatrolState = new EnemyPatrolState(this, animator, stateMachine);
            ChaseState = new EnemyChaseState(this, animator, stateMachine);
            AttackState = new EnemyAttackState(this, animator, stateMachine);
            CoolDownState = new EnemyCoolDownState(this, animator, stateMachine);
            ComboState = new EnemyComboState(this, animator, stateMachine);

            stateMachine.ChangeState(GetIdleFor(configSO.restTime, PatrolState));
        }

        private void Update()
        {

            stateMachine.Update();
        }

        //为什么不把移动逻辑放在基类，听说基类要放更通用的接口
        //通用行为还是放这里吧
        //这里是可能好几个状态会用到的函数...好吧...
        //所以我决定写个move模块

        public IEnemyState GetIdleFor(float time, IEnemyState state)
        {
            IdleState.SetIdle(time, state);
            return IdleState;
        }

#if UNITY_EDITOR
        [ContextMenu("Refresh SO Data")]
        private void RefreshRuntimeData()
        {
            runtimeData.RefreshFrom(configSO);
        }
#endif
        public void OnHatJustHit()
        {
            if (stateMachine.currentState is EnemyAttackState attackState)
            {
                attackState.OnAnimationEvent_Hit();
            }
            else if (stateMachine.currentState is EnemyComboState comboState)
            { 
                comboState.OnAnimationEvent_Hit();
            }
        }

        public void OnAttackEnd()
        {
            if (stateMachine.currentState is EnemyAttackState attackState)
            {
                attackState.OnAnimationEvent_AnimEnd();
            }
            else if (stateMachine.currentState is EnemyComboState comboState)
            {
                comboState.OnAnimationEvent_AnimEnd();
            }
        }

        //有待模块化
        //temp
        //有很多要修改的地方
        private UI_EnemyHealthBar healthBarScript;
        private float maxHealth = 999.0f;
        private float currentHealth = 999f;

        public void TakeDamage(float amout)
        {
            //Debug.Log("Yellow Duck is Coming");
            if (healthBar == null)
            {
                healthBar = ObjectPoolManager.Instance.Get(configSO.HealthBar);
                healthBar.transform.SetParent(WorldUICanvas.Instance.transform);

                var script = healthBar.GetComponent<UI_EnemyHealthBar>();
                script.Init(transform, Vector3.up * 2.5f);
                healthBarScript = script;
                //healthBar.transform.position = transform.position + Vector3.up * 2.0f;
            }
            currentHealth -= amout;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            healthBarScript?.OnDamageTaked(currentHealth, maxHealth);

            //这是临时的，以后会做deadState
            //另外会做篝火重生模式
            if (currentHealth <= 0)
            {
                var vanish = ObjectPoolManager.Instance.Get(vanishEffect);
                vanish.transform.position = returnPointer.position;

                healthBar.SetActive(false);
                Destroy(gameObject);
            }
        }

        //Agent Interface. will be module later
        public void SetDestination(Vector3 pos)
        {
            Agent.SetDestination(pos);
        }

        public void StopMoving()
        {
            Agent.ResetPath();
        }

        public bool IsAtDestination(float threshold = 0.5f)
        {
            if (!Agent.hasPath) return true;
            return Agent.remainingDistance <= threshold;
        }
    }
}
