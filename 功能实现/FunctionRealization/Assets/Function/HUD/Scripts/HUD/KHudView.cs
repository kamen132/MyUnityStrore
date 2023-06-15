using Common.CommonScripts.SampleUIFramework;
using UnityEngine;

namespace Function.HUD.Scripts.HUD
{
    public class KHudView : UIBaseWindow
    {
        private Camera mTargetCamera;
        private Camera mUICamera;
        /// <summary>
        /// 设置相机
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="target"></param>
        public void SetCamera(Camera ui,Camera target)
        {
            mUICamera = ui;
            mTargetCamera = target;
        }

        public void SetHudTarget(UIBaseWindow hudItem,Transform follow)
        {
            
        }
    }
}