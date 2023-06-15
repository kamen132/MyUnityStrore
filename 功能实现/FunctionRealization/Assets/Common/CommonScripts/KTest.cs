using System.Collections;
using System.Collections.Generic;
using Common.CommonScripts.SampleUIFramework;
using Function.HUD.Scripts.HUD;
using UnityEngine;

public class KTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.Open<KHudView>(null, UILayer.Window);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
