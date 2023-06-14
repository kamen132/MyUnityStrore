using UnityEngine;

namespace Function.HUD.Scripts
{
    public class HudTest : MonoBehaviour
    {
        //Hud节点
        public RectTransform HudRect;
        //Hud跟随目标点
        public GameObject HudTarget;
        //UI相机
        public Camera UICamera;
        //跟随目标相机
        public Camera TargetCamera;
    
        void Update()
        {
            //转换屏幕坐标
            var screenPos = RectTransformUtility.WorldToScreenPoint(TargetCamera, HudTarget.transform.position);
            Vector2 localPos;
            //参数：1.HUD的节点的RectTransForm  2.屏幕坐标  3.UI相机
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(HudRect.transform.parent.GetComponent<RectTransform>(), screenPos, UICamera, out localPos))
            {
                HudRect.anchoredPosition = localPos;
            }
        }
    }
}