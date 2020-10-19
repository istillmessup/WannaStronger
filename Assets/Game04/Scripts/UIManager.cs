using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game04
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _instance;

        public int flag; // 根据flag不同的值来设置不同类型的路径：0起点、1沿途点、2终点
        public Button setStart; // 设置起点按钮

        private void Awake()
        {
            _instance = this;
            flag = -1;
            setStart.onClick.AddListener(() =>
            {
                flag = 0;
                setStart.interactable = false;
            });
        }
    }
}

