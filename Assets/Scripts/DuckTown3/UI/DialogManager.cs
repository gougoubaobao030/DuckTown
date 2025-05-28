using System;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

    [SerializeField]private UIConfirmDialogue comfirmDialog;

    public static DialogManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ShowComfirmMessage(string message, Action confirmAction)
    { 
        comfirmDialog.Show(message, confirmAction);
    }
}
