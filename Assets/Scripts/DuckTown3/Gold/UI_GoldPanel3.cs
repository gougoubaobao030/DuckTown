using TMPro;
using UnityEngine;

public class UI_GoldPanel3 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Amout;

    private IGoldSystem goldSystem;

    public void Init(IGoldSystem goldSystem)
    { 
        this.goldSystem = goldSystem;
        this.goldSystem.OnGoldAmoutChanged += UpdateUI;
        UpdateUI(this.goldSystem.Gold);
        //Debug.Log(this.goldSystem.Gold);
    }

    private void OnEnable()
    {
        //Debug.Log("UI_GoldPanel3 OnEnable");
    }

    private void OnDisable()
    { 
        if (goldSystem != null) 
        goldSystem.OnGoldAmoutChanged -= UpdateUI;
        //Debug.Log("UI_GoldPanel3 OnDisEnable");
    }

    private void UpdateUI(int amount)
    {
        //Debug.Log("Add 100");
        Amout.text = amount.ToString();
    }

}
