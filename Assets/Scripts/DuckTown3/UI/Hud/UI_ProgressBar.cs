using UnityEngine;
using UnityEngine.UI;

namespace DuckTown3.UI
{
    public class UI_ProgressBar : MonoBehaviour, IUIProgressBar
    {

        [SerializeField]private Slider slider;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            slider.value = 0f;
            gameObject.SetActive(true);
        }

        public void UpdateProgress(float percent)
        {
            slider.value = Mathf.Clamp01(percent);
        }

        //TODO: スキン切り替え機能を実装予定
        public void SetSkin(Sprite background, Sprite fill)
        {
            if (slider.targetGraphic is Image bg)
                bg.sprite = background;

            if (slider.fillRect.TryGetComponent<Image>(out var fillImage))
                fillImage.sprite = fill;
        }
    }
}
