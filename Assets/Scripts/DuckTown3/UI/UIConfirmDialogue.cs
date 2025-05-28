using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIConfirmDialogue : MonoBehaviour
{
    [SerializeField]private Button cancelBtn;
    [SerializeField]private Button OKBtn;
    [SerializeField]private TextMeshProUGUI infoToShow;

    private Action OnComfired;
    private void Awake()
    {
        //gameObject.SetActive(false);
        cancelBtn.onClick.AddListener(() => gameObject.SetActive(false));
        OKBtn.onClick.AddListener(OnOkButtenClicked);
    }

    private void OnOkButtenClicked()
    {
        OnComfired?.Invoke();
        gameObject.SetActive(false);
    }

    public void Show(string message, Action comfirmAction)
    { 
        infoToShow.text = message;
        OnComfired = comfirmAction;
        gameObject.SetActive(true);
    }
}
