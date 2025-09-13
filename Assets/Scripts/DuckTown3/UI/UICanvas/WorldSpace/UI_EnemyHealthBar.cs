using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
//Simplified for testing purposes
namespace DuckTown3.UI
{
    public class UI_EnemyHealthBar : MonoBehaviour
    {
        [SerializeField]private Image image;

        //follow
        private Camera mainCam;
        private Transform followTarget;
        private Vector3 offset;

        //hide healthbar
        [SerializeField]private CanvasGroup canvasGroup;
        private float lastHitTime;
        private float hideDelayTime = 3.0f;
        private bool isVisible = false;

        private Tween fadeTween;

        private void Start()
        {
            mainCam = Camera.main;
            //HideImmediate();
        }

        public void Init(Transform target, Vector3 offset)
        { 
            followTarget = target;
            this.offset = offset;
        }

        private void LateUpdate()
        {
            if (mainCam == null)
            {
                Debug.Log("has no mainCam");
                return;
            }

            transform.position = followTarget.position + offset;

            transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);

            if (isVisible && Time.time - lastHitTime > hideDelayTime)
            {
                Hide();
            }
            
        }

        public void OnDamageTaked(float current, float max)
        { 
            lastHitTime = Time.time;

            if (image != null)
            {
                float targetValue = (float)current / max;

                image.DOKill();

                image.DOFillAmount(targetValue, 0.3f).SetEase(Ease.OutCubic);

                //image.fillAmount = (float)current / max; 
            }

            if (isVisible == false)
            {
                Show();
            }
        }

        private void Show()
        { 
            isVisible = true;
            //canvasGroup.alpha = 1.0f;
            //Debug.Log("is show");

            fadeTween?.Kill();
            fadeTween = canvasGroup.DOFade(1f, 0.3f);
        }

        private void Hide()
        { 
            isVisible = false;
            //canvasGroup.alpha = 0.0f;

            fadeTween?.Kill();
            fadeTween = canvasGroup.DOFade(0f, 0.5f);
        }

        private void HideImmediate()
        { 
            canvasGroup.alpha = 0.0f;
            isVisible = false;
        }
    }
}
