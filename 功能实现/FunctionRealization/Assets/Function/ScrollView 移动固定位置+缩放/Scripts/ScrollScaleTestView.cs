using System.Collections;
using System.Collections.Generic;
using Common.CommonScripts;
using UnityEngine;

public class ScrollScaleTestView : FunctionBaseView
{
    public RectTransform target;
    public ScrollScale ScrollScale;
    private void Start()
    {
        mBtn1.BtnTxt = "MoveTo";
    }

    protected override void OnBtn1Click()
    {
        base.OnBtn1Click();
        ScrollScale.MoveToTarget(target,1);
    }
}
