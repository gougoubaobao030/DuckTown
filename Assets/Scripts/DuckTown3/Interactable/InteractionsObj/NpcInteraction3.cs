﻿using UnityEngine;

public class NpcInteraction : MonoBehaviour, IInteractable
{
    private bool isTalking = false;

    public InteractMode InteractMode => InteractMode.State;

    private void Start()
    {
        
    }

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractPrompt()
    {
        return "这是一个可以说话的方块，按E交流";
    }

    public void Interact()
    {
        Debug.Log("一切皆善");
        //这个代码应该没人在用
        //InteractionEvents.TriggerInteractionStarted();
        UI_DialogueManager3.instance.ShowDialoguePanel();
        isTalking = true;
        var lookDir = Duck3.instance.transform.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    private void Update()
    {
        if (isTalking && Vector3.Distance(transform.position, Duck3.instance.transform.position) > 5.5)
        {
            UI_DialogueManager3.instance.CloseDialoguePanel();
            isTalking = false;
        }
    }

}
