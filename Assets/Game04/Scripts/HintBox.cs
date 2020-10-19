using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game04
{
    public class HintBox : MonoBehaviour
    {
        public static HintBox _instance;
        public Text text; // 提示信息
        public Button button; // 确认按钮

        private void Awake()
        {
            _instance = this;
            transform.localScale = Vector3.zero;
            button.onClick.AddListener(() =>
            {
                transform.DOScale(0, .3f);
            });
        }

        public void ShowMessage(string message)
        {
            transform.DOScale(1, .3f);
            text.text = message;
        }
    }
}

