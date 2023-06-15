using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMarchItem : MonoBehaviour
{
    public RectTransform hud; //Hud
    public RectTransform canvas;//UI的父节点
    public Transform parent; //跟随的3D物体
    public Camera uiCamera; //UICamera

    Vector3 offset; //hud偏移量
    Vector3 cachePoint;
    float originalDistance;
    float factor = 1;
    bool visiable = true;

    void Start()
    {
        offset = hud.localPosition - WorldPointToUILocalPoint(parent.position);
        cachePoint = parent.position;
        originalDistance = GetCameraHudRootDistance();
        UpdateVisible();
    }

    void LateUpdate()
    {
        if (cachePoint != parent.position)
        {
            float curDistance = GetCameraHudRootDistance();
            factor = originalDistance / curDistance;
            UpdatePosition(); //更新Hud位置
            UpdateScale(); //更新Hud的大小
            UpdateVisible(); //更新Hud是否可见，根据需求设置：factor或者根据和相机距离设置，一定范围内可见，相机视野范围内可见 等等
        }
    }


    private void UpdateVisible()
    {

    }

    private void UpdatePosition()
    {
        hud.localPosition = WorldPointToUILocalPoint(parent.position) + offset * factor;
        cachePoint = parent.position;
    }

    private void UpdateScale()
    {
        hud.localScale = Vector3.one * factor;
    }

    private float GetCameraHudRootDistance()
    {
        return Vector3.Distance(Camera.main.transform.position, parent.position);
    }

    private Vector3 WorldPointToUILocalPoint(Vector3 point)
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(point);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPoint, uiCamera, out localPoint);
        return localPoint;
    }
}
