using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DuckTown3.UI
{
    public class UI_PopText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI popMessage;
        [SerializeField] private CanvasGroup canvas;

        private Tween currentTween;

        private void Awake()
        {
            canvas.alpha = 0.0f;
        }

        public void Show(string msg)
        {
            popMessage.text = msg;
            canvas.alpha = 0.0f;

            currentTween?.Kill();

            currentTween = DOTween.Sequence()
                .Append(canvas.DOFade(1.0f, 0.3f))
                .AppendInterval(1.0f)
                .Append(canvas.DOFade(0.0f, 1.5f));
        }
    }
}
