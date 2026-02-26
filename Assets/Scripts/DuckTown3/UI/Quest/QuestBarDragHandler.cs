using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

//试图实现任务栏Header的拖拽功能
//这样就可以随意放置任务栏

[DisallowMultipleComponent]
public class QuestBarDragHandler : MonoBehaviour,
    IPointerDownHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IPointerClickHandler
{
    //drag
    [SerializeField] private RectTransform targetPanel;
    private Vector2 offset;

    //收起任务栏
    private bool isExpanding = false;
    [SerializeField] private ScrollRect scrollRect;
    //rotate arrow
    //image 本身并没有旋转信息
    [SerializeField] private RectTransform arrowIcon;

    private void Start()
    {
        isExpanding = scrollRect.gameObject.activeSelf;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 记录鼠标在面板内的相对位置
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetPanel,
            eventData.position,
            eventData.pressEventCamera,
            out offset
        );
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 拖拽开始时可以降低透明度、提高排序
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPos;
        //当转换成功
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetPanel.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pointerPos))
        {
            //鼠标相对于父坐标的位置 - 鼠标对于拖动物体的offset = 最终该recttransform最后在的位置
            targetPanel.localPosition = pointerPos - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 拖拽结束时可回归透明度或播放惯性动画
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.dragging) return;

        //if (isExpanding)
        //{
        //    scrollRect.gameObject.SetActive(false);
        //}
        //else
        //{ 
        //    scrollRect.gameObject.SetActive(true);
        //}
        isExpanding = !isExpanding;
        scrollRect.gameObject.SetActive(isExpanding);
        RotateArrow(isExpanding);
    }

    private void RotateArrow(bool isExpanding)
    {
        float targetAngle = isExpanding ? 0f : 90f;

        arrowIcon.DOKill();
        arrowIcon.DOLocalRotate(new Vector3(0, 0, targetAngle), 0.2f).SetEase(Ease.OutQuad);
        //setease 有点曲线
    }
}
