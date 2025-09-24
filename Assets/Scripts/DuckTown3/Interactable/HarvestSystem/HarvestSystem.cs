using DuckTown3.UI;
using System;
using UnityEngine;

//logic layer
namespace DuckTown3.Interact
{
    public class HarvestSystem : IHarvest
    {
        //timer
        private Action onComplete;
        private IUIProgressBar harvestUI;
        private float timer = 0.0f;
        private float duration = 0.0f;

        public bool IsInProgress { get; private set; } = false;

        public HarvestSystem(IUIProgressBar ui)
        { 
            harvestUI = ui;
        }

        public void Cancel()
        {
            if (IsInProgress == false) return;

            IsInProgress = false;
            harvestUI?.Hide();
            onComplete = null;
        }

        public void Start(Harvestable harvest, float duration, Action onComplete)
        {
            //防御的プログラミング
            //1. check status
            if(IsInProgress) return;
            //2.reset var
            //3.store
            timer = 0.0f;
            this.duration = duration;
            this.onComplete = onComplete;

            //4.mark status
            IsInProgress = true;
            //5.prepare ui
            harvestUI?.UpdateProgress(0.0f);
            harvestUI?.Show();
        }

        public void Tick(float deltaTime)
        {
            //防御的プログラミング
            if (IsInProgress == false) return;

            //calc time, pass to ui
            timer += deltaTime;
            float percent = timer / duration;
            harvestUI.UpdateProgress(percent);

            if (timer > duration)
            { 
                IsInProgress = false;
                harvestUI?.Hide();
                onComplete?.Invoke();
            }
        }
    }
}
