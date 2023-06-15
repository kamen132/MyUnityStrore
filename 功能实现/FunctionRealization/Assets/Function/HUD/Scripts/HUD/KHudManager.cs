using System.Collections.Generic;
using Common.CommonScripts.SampleUIFramework;
using KFramework.Common;

namespace Function.HUD.Scripts.HUD
{
    public class KHudManager : MonoSingleton<KHudManager>
    {
        private readonly Dictionary<UIBaseWindow, KHud> mHudMap = new Dictionary<UIBaseWindow, KHud>();
        
    }
}