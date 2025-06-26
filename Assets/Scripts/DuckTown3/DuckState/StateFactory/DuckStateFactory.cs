using UnityEngine;
using System.Collections.Generic;
using System;

//legacy now
public class DuckStateFactory
{
    private DuckControllerV3 duck;
    private Dictionary<Type, IDuckState> cacheDuckStates = new();

    public DuckStateFactory(DuckControllerV3 duck)
    { 
        this.duck = duck;
    }

    public T Create<T>() where T : IDuckState
    {
        Type type = typeof(T);

        if (cacheDuckStates.TryGetValue(type, out IDuckState duckState))
        { 
            return (T)duckState;
        }

        //百分90的开发场景，分开写好
        IDuckState newState = CreateNewState(type);
        cacheDuckStates[type] = newState;

        //T是具体类型，因为这是对外的具体接口
        return (T)newState;
    }

    private IDuckState CreateNewState(Type type)
    {
        switch (type)
        {
            //case var t when t == typeof(DuckIdleState):
                //return new DuckIdleState(duck);
            //case var t when t == typeof(DuckMoveState):
                //return new DuckMoveState(duck);
            default:
                throw new ArgumentException($"DuckStateFactory has not impt：{type.Name}");
        }

        //throw 就直接等于掀桌子了，还return
        //return个Jb
    }
}
