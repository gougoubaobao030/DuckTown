using UnityEngine;

namespace DuckTown3
{
    public class EnemyStateMachine
    {
        public IEnemyState currentState { get; private set; }

        public void ChangeState(IEnemyState newState)
        {
            currentState?.Exit();
            currentState = newState;
            newState.Enter();
        }

        public void Update()
        { 
            currentState?.Update();
        }
    }
}
