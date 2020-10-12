using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game03
{
    public class GridController : MonoBehaviour
    {
        public int x, y;
        public Color color;

        private Vector3 positionStart;
        private Vector3 positionEnd;
        private bool isMouseDown = false;
        private void Update()
        {
            if (isMouseDown && Input.GetMouseButtonUp(0) && MapManager.flag)
            {
                positionEnd = Input.mousePosition;
                Vector3 delta = positionEnd - positionStart;
                isMouseDown = false;
                // 这些移动控制代码可以尝试用函数包装一下
                if (delta.x < 0 && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    GetComponentInParent<MapManager>().GridLeftMove(y);
                    Check();
                }
                if (delta.x > 0 && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    GetComponentInParent<MapManager>().GridRightMove(y);
                    Check();
                }
                if (delta.y < 0 && Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
                {
                    GetComponentInParent<MapManager>().GridDownMove(x);
                    Check();
                }
                if (delta.y > 0 && Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
                {
                    GetComponentInParent<MapManager>().GridUpMove(x);
                    Check();
                }
            }
        }

        public void OnPointerDown()
        {
            if (MapManager.flag)
            {
                isMouseDown = true;
                positionStart = Input.mousePosition;
            }
        }

        private void Check()
        {
            if (MapManager._instance.Check())
            {
                Debug.Log("game over");
            }
        }
    }
}

