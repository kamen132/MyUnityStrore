using System;
using System.Collections.Generic;
using KFramework.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Common.CommonScripts.SampleUIFramework
{
    public class UIManager : MonoSingleton<UIManager>
    {
        private Transform uiRoot;

        //UI层级父节点
        public Transform UIRoot => uiRoot;
        private Dictionary<string, UIBase> mUIMap = new Dictionary<string, UIBase>();
        private Dictionary<UILayer, GameObject> mLayerMap = new Dictionary<UILayer, GameObject>();

        /// <summary>
        /// 打开一个界面
        /// </summary>
        /// <param name="winInfo">界面信息</param>
        /// <param name="layer">层级</param>
        /// <typeparam name="T">UIBase</typeparam>
        public string Open<T>(UIWinInfo winInfo, UILayer layer) where T : UIBase, new()
        {
            UIWinInfo info = UIWinInfo.Create(winInfo);
            T win = CreateWindow<T>(info, layer);
            string winName = win.WindowName;
            mUIMap[winName] = win;
            win.OnOpen();
            return winName;
        }

        /// <summary>
        /// 创建一个界面
        /// </summary>
        /// <param name="path"></param>
        /// <param name="winInfo"></param>
        /// <param name="layer"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateWindow<T>(UIWinInfo winInfo, UILayer layer) where T : UIBase, new()
        {
            T win = new T();
            string path = "UI/GameUIPrefab/" + typeof(T).Name;
            GameObject viewObj = GameObject.Instantiate(Resources.Load<GameObject>(path));
            if (viewObj == null)
            {
                Debug.LogError("没找到对应界面Prefab path:" + path);
                return null;
            }

            mLayerMap.TryGetValue(layer, out var uiParent);
            viewObj.transform.SetParent(uiParent.transform,false);
            win.Create(path, typeof(T).Name, winInfo, layer);
            return win;
        }

        public void RemoveView(string winName)
        {
            if (mUIMap.ContainsKey(winName))
            {
                mUIMap.Remove(winName);
            }
        }

        protected override void AwakeEx()
        {
            base.AwakeEx();
            CreateLayer();
        }

        /// <summary>
        /// 创建UI层级
        /// </summary>
        private void CreateLayer()
        {
            uiRoot = GameObject.Find("UIRoot").transform;
            foreach (UILayer layer in Enum.GetValues(typeof(UILayer)))
            {
                GameObject go = new GameObject("U_" + layer);
                go.layer = uiRoot.gameObject.layer;
                RectTransform layerTran = go.AddComponent<RectTransform>();
                layerTran.SetParent(uiRoot.transform);
                layerTran.localPosition = Vector3.zero;
                layerTran.localScale = Vector3.one;
                layerTran.anchorMin = new Vector2(0f, 0f);
                layerTran.anchorMax = new Vector2(1f, 1f);
                layerTran.offsetMin = new Vector2(0f, 0f);
                layerTran.offsetMax = new Vector2(0f, 0f);
                int sortingOrder = (int) layer * 1000;
                if (sortingOrder > 0)
                {
                    Canvas layerCanvas = go.AddComponent<Canvas>();
                    layerCanvas.overrideSorting = true;
                    layerCanvas.sortingOrder = sortingOrder;
                    go.AddComponent<GraphicRaycaster>();
                }

                mLayerMap.Add(layer, go);
            }
        }
    }
}