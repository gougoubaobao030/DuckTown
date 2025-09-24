using System;
using UnityEngine;

namespace DuckTown3.Interact
{
    public interface IHarvest
    {
        void Start(Harvestable harvest, float duration, Action onComplete);
        void Tick(float deltaTime); //void Tick(float DeltaTime)
        void Cancel();
        bool IsInProgress {get;}
    }
}
