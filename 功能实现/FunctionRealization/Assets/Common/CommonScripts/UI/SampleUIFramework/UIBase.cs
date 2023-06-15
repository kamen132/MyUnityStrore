using UnityEngine;

namespace Common.CommonScripts.SampleUIFramework
{
    public abstract class UIBase
    {
        public abstract string WindowName { get; protected set; }
        public abstract void OnOpen();
        public abstract void OnClose();
        public abstract void OnDestroy();

        public abstract void Create(string path, string uiName,UIWinInfo winInfo, UILayer layer);
    }
}