using Unity.VisualScripting;

namespace Common.CommonScripts.SampleUIFramework
{
    public class UIWinInfo
    {
        public object Param;

        public static UIWinInfo Create(object param)
        {
            UIWinInfo winInfo = new UIWinInfo();
            winInfo.Param = param;
            return winInfo;
        }
    }
}