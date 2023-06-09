using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂在到ScrollView
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public class ScrollScale : MonoBehaviour
{
    //手势缩放速度
    public float speedPinch = 0.001f;
    //鼠标滚轮缩放速度
    public float speedMouseScrollWheel = 0.05f;
    public float scaleMin = 1f;
    public float scaleMax = 2f;
    private RectTransform contentRect;
    private ScrollRect scrollRect;
    private RectTransform viewport;
    private RectTransform content;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        viewport = scrollRect.viewport;
        content = scrollRect.content;
        contentRect = content.GetComponent<RectTransform>();
    }

    public struct ScrollScaleParam
    {
        public float ScaleMin;
        public float ScaleMax;
        public float InitScale;
    }

    public void SetScale(ScrollScaleParam param, Action call = null)
    {
        scaleMax = param.ScaleMax;
        scaleMin = param.ScaleMin;
        content.DOScale(param.InitScale, 0).OnComplete(() => { call?.Invoke(); });
    }

    public void MoveToTarget(RectTransform item, float moveTime)
    {
        float scale = content.transform.localScale.x;
        Vector3 itemCurrentLocalPostion = scrollRect.GetComponent<RectTransform>().InverseTransformVector(ConvertLocalPosToWorldPos(item));
        Vector3 itemTargetLocalPos = scrollRect.GetComponent<RectTransform>().InverseTransformVector(ConvertLocalPosToWorldPos(viewport));

        Vector3 diff = (itemTargetLocalPos - itemCurrentLocalPostion);
        diff.z = 0.0f;

        var newNormalizedPosition = new Vector2(
            diff.x / (content.GetComponent<RectTransform>().rect.width * scale - viewport.rect.width),
            diff.y / (content.GetComponent<RectTransform>().rect.height * scale - viewport.rect.height)
        );

        newNormalizedPosition = scrollRect.GetComponent<ScrollRect>().normalizedPosition - newNormalizedPosition;

        newNormalizedPosition.x = Mathf.Clamp01(newNormalizedPosition.x);
        newNormalizedPosition.y = Mathf.Clamp01(newNormalizedPosition.y);

        DOTween.To(() => scrollRect.GetComponent<ScrollRect>().normalizedPosition, x => scrollRect.GetComponent<ScrollRect>().normalizedPosition = x, newNormalizedPosition, moveTime);
    }


    private void LateUpdate()
    {
        Zoom();
    }

    private Vector3 ConvertLocalPosToWorldPos(RectTransform target)
    {
        var pivotOffset = new Vector3(
            (0.5f - target.pivot.x) * target.rect.size.x,
            (0.5f - target.pivot.y) * target.rect.size.y,
            0f);

        var localPosition = target.localPosition + pivotOffset;

        return target.parent.TransformPoint(localPosition);
    }

    void Zoom()
    {
        var mouseScrollWheel = Input.mouseScrollDelta.y;
        float scaleChange = 0f;
        Vector2 midPoint = Vector2.zero;
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

            scaleChange = deltaMagnitudeDiff * speedPinch;

            midPoint = (touchOne.position + touchZero.position) / 2;
        }

        if (mouseScrollWheel != 0)
        {
            scaleChange = mouseScrollWheel * speedMouseScrollWheel;
            midPoint = Input.mousePosition;
        }

        if (scaleChange != 0)
        {
            var scaleX = content.localScale.x;
            scaleX += scaleChange;
            scaleX = Mathf.Clamp(scaleX, scaleMin, scaleMax);
            var size = contentRect.rect.size;
            size.Scale(contentRect.localScale);
            var parentRect = ((RectTransform) contentRect.parent);
            var parentSize = parentRect.rect.size;
            parentSize.Scale(parentRect.localScale);
            if (size.x > parentSize.x && size.y > parentSize.y)
            {
                var p1 = Camera.main.ScreenToWorldPoint(midPoint);
                var p2 = transform.InverseTransformPoint(p1);
                var pivotP = contentRect.pivot * contentRect.rect.size;
                var p3 = (Vector2) p2 + pivotP;
                var newPivot = p3 / contentRect.rect.size;
                newPivot = new Vector2(Mathf.Clamp01(newPivot.x), Mathf.Clamp01(newPivot.y));
                contentRect.SetPivot(newPivot);
            }
            else
            {
                contentRect.SetPivot(new Vector2(0.5f, 0.5f));
            }

            content.localScale = new Vector3(scaleX, scaleX, content.localScale.z);
        }
    }
}
