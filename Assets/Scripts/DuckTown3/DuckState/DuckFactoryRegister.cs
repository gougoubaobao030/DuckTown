using UnityEngine;
using System.Collections.Generic;
using System;

public class DuckFactoryRegister
{
    private DuckControllerV3 duck;
    private DuckStateMachineWithFactory stateMachine;
    private Dictionary<Type, IDuckState> cacheDuckState = new();
    private Dictionary<Type, Func<IDuckState>> registorDuckState = new();

    public DuckFactoryRegister(DuckControllerV3 duck)
    {
        this.duck = duck;

        Register<DuckIdleState>(() => new DuckIdleState(duck, stateMachine));
        Register<DuckMoveState>(() => new DuckMoveState(duck, stateMachine));
        Register<DuckJumpState>(() => new DuckJumpState(duck, stateMachine));
        Register<DuckFallState>(() => new DuckFallState(duck, stateMachine));
        //Register<DuckDashState>(() => new DuckDashState(duck, stateMachine));
        Register<DuckDashState>(() => new DuckDashState(duck, stateMachine, () => GameManager.Instance.VFXPoolManager.GetDashEffect()));
        Register<DuckBlinkDodgeState>(() => new DuckBlinkDodgeState(duck, stateMachine));
        //and so on.
    }

    //delay constructor
    public void SetStateMachine(DuckStateMachineWithFactory sm)
    { 
        this.stateMachine = sm;
    }

    private void Register<T>(Func<IDuckState> creator) where T : IDuckState
    { 
        Type type = typeof(T);
        if (registorDuckState.ContainsKey(type))
        {
            Debug.LogWarning($"[DuckFactoryPraci] type {type.Name} registered。");
        }

        registorDuckState[typeof(T)] = creator;
    }

    public T Create<T>() where T : IDuckState
    { 
        Type type = typeof(T);

        if (cacheDuckState.TryGetValue(type, out var state))
        {
            return (T)state;
        }

        if (!registorDuckState.TryGetValue(type, out var creator))
        { 
            throw new ArgumentException($"DuckStateFactory 没注册类型：{type.Name}");
        }

        IDuckState newState = creator();
        cacheDuckState[type] = newState;
        return (T)newState;
    }
}
