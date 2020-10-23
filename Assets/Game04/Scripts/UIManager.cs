using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game04
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _instance;

        [HideInInspector]
        public int flag; // 根据flag不同的值来设置不同类型的路径：0起点、1沿途点
        public Button setStart; // 设置起点按钮
        public Button setEnd; // 设置终点按钮
        public Button save; // 保存路径为json文件
        public Button undo; // 撤销

        private void Awake()
        {
            _instance = this;
            flag = -1;
            setStart.onClick.AddListener(() =>
            {
                flag = 0;
                setStart.interactable = false;
            });
            setEnd.onClick.AddListener(() =>
            {
                if (MapManager._instance.pointList.Count == 1)
                {
                    HintBox._instance.ShowMessage("路径太短了");
                    return;
                }
                flag = -1;
                setEnd.interactable = false;
                MapManager._instance.SetEndPoint();
            });
            save.onClick.AddListener(() =>
            {
                Json.Save(MapManager._instance.pointList);
            });
            undo.onClick.AddListener(() =>
            {
                MapManager._instance.Undo();
            });
        }
    }
}

