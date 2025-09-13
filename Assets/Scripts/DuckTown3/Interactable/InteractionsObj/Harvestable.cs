using UnityEngine;
using DuckTown3.ObjectPool;

namespace DuckTown3.Interact
{
    public class Harvestable : MonoBehaviour, IInteractable
    {

        [SerializeField] private GameObject blastMultiColor;

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
            Destroy(gameObject);
            var blast = ObjectPoolManager.Instance.Get(blastMultiColor);
            blast.transform.position = transform.position;
        }
    }
}