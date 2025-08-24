using TMPro;
using UnityEngine;
using DG.Tweening;

public class UI_GoldPanel3 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Amout;
    private int currentAmount = 0;

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
        //Amout.text = amount.ToString();

        // 停止之前未完成的动画，避免叠加
        DOTween.Kill(Amout, complete: false);

        // 动画：currentAmount → newAmount
        DOTween.To(() => currentAmount, x =>
        {
            currentAmount = x;
            Amout.text = currentAmount.ToString();
        }, amount, 0.8f) // 0.8 秒完成动画
        .SetEase(Ease.OutCubic)
        .SetTarget(Amout); // 绑定目标，方便 Kill
    }

}
