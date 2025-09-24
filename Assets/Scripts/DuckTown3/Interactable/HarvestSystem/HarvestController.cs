using DuckTown3.UI;
using System;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

//controller layer
namespace DuckTown3.Interact
{
    public class HarvestController : MonoBehaviour
    {
        //ui inject
        [SerializeField]private MonoBehaviour harvestUI;
        [SerializeField]private float havMaxDis = 3.0f;
        [field: SerializeField] public float harvestTime { get; private set; } = 2.0f;
        private IUIProgressBar ui;
        private IHarvest harvestSystem;

        private Harvestable currentHarvest;

        private void Start()
        {
            //change monoui to Iui;
            //Debug.Log($"harvestUI type = {harvestUI.GetType()}");
            //↑ it shows slider not Iuiprogressbar;
            ui = harvestUI as IUIProgressBar;
            if (ui != null)
            {
                harvestSystem = new HarvestSystem(ui);
            }
            else
            {
                Debug.LogError("UI not impl Igrogress or not injected!");
            }
        }
        //update time
        private void Update()
        { 
            harvestSystem?.Tick(Time.deltaTime);
            if (currentHarvest != null)
            {
                float dist = Vector3.Distance(currentHarvest.transform.position, Duck3.instance.transform.position);
                if (dist > havMaxDis)
                { 
                    CancelHarvest();
                }
            }
        }

        //facade startharvest or wrapper
        public void StartHarvest(Harvestable harvestable, float duration, Action onComplete)
        {
            currentHarvest = harvestable;
            harvestSystem?.Start(harvestable, duration, onComplete);
        }

        public void CancelHarvest()
        { 
            harvestSystem?.Cancel();
            currentHarvest = null;  
        }
        //wrapper
        public bool IsHarvesting => harvestSystem != null && harvestSystem.IsInProgress;
    }
}
