using System;

namespace Common.CommonScripts.SampleUIFramework
{
    public class UIBaseWindow : UIBase
    {
        public UIWinInfo Info { get; set; }
       
        public bool IsOpen;
        public override string WindowName { get; protected set; }

        public override void OnOpen()
        {
           
        }

        public void Close()
        {
            if (IsOpen)
            {
                UIManager.Instance.RemoveView(WindowName);
                OnClose();
                IsOpen = false;
            }
        }

        public override void OnClose()
        {
            
        }

        public override void OnDestroy()
        {
          
        }

        public override void Create(string path, string uiName, UIWinInfo winInfo, UILayer layer)
        {
            WindowName = uiName;
            IsOpen = true;
        }
        
    }
}