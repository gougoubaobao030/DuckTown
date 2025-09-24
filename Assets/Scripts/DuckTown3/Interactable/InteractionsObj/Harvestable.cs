using UnityEngine;
using DuckTown3.ObjectPool;

namespace DuckTown3.Interact
{
    public class Harvestable : MonoBehaviour, IInteractable
    {

        [SerializeField] private GameObject blastMultiColor;
        [SerializeField] private HarvestController harvestController;
        [SerializeField] private string msg = "Mushroom collected successfully!";

        //之后要改成InteractMode.State
        //现在先暂时InteractMode.Oneshotl;
        public InteractMode InteractMode => InteractMode.OneShot;

        public bool CanInteract()
        {
            throw new System.NotImplementedException();
        }

        public string GetInteractPrompt()
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {
            //Debug.Log("Harvasting Time!");
            //Destroy(gameObject);
            //var blast = ObjectPoolManager.Instance.Get(blastMultiColor);
            //blast.transform.position = transform.position;
            if(harvestController == null || harvestController.IsHarvesting) return;
            harvestController.StartHarvest(this, harvestController.harvestTime, OnHarvestCompleted);
        }

        private void OnHarvestCompleted()
        {
            var blast = ObjectPoolManager.Instance.Get(blastMultiColor);
            blast.transform.position = transform.position;
            Destroy(gameObject);

            GameManager.Instance.UIManager.PopTest(msg);
        }

        //这样会有很多个检测
        //private void Update()
        //{
        //    float distance = Vector3.Distance(transform.position, Duck3.instance.transform.position);
        //    Debug.Log(distance);
        //    if (harvestController.IsHarvesting && distance > 3f)
        //    {
        //        harvestController.CancelHarvest();
        //    }
        //}
    }
}