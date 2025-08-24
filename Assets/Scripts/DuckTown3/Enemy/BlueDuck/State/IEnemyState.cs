using UnityEngine;


namespace DuckTown3
{
    public interface IEnemyState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
