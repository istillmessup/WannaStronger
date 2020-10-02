using UnityEngine;

namespace Game01
{
    public class GridController : MonoBehaviour
    {
        public int x, y;
        public Vector2 currPoint; // 为什么用point而不是position：这里存储的是坐标，比如(1,2)，而不是具体位置(100,200)

        private Vector3 positionStart;
        private Vector3 positionEnd;
        private bool isMouseDown = false;

        private void Update()
        {
            if (isMouseDown && Input.GetMouseButtonUp(0))
            {
                positionEnd = Input.mousePosition;
                Vector3 delta = positionEnd - positionStart;
                isMouseDown = false;
                // 这些移动控制代码可以尝试用函数包装一下
                if (delta.x < 0 && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (currPoint.x == 0)
                    {
                        return;
                    }
                    Vector2 nextPoint = new Vector2(currPoint.x - 1, currPoint.y);
                    MapManager._instance.GridMove(ref currPoint, nextPoint);
                    Check();
                }
                if (delta.x > 0 && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (currPoint.x == x - 1)
                    {
                        return;
                    }
                    Vector2 nextPoint = new Vector2(currPoint.x + 1, currPoint.y);
                    MapManager._instance.GridMove(ref currPoint, nextPoint);
                    Check();
                }
                if (delta.y < 0 && Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
                {
                    if (currPoint.y == 0)
                    {
                        return;
                    }
                    Vector2 nextPoint = new Vector2(currPoint.x, currPoint.y - 1);
                    MapManager._instance.GridMove(ref currPoint, nextPoint);
                    Check();
                }
                if (delta.y > 0 && Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
                {
                    if (currPoint.y == y - 1)
                    {
                        return;
                    }
                    Vector2 nextPoint = new Vector2(currPoint.x, currPoint.y + 1);
                    MapManager._instance.GridMove(ref currPoint, nextPoint);
                    Check();
                }
            }
        }

        public void OnPointerDown()
        {
            isMouseDown = true;
            positionStart = Input.mousePosition;
        }

        // 在每次移动结束后判断棋盘是否完成
        private void Check()
        {
            if (MapManager._instance.Check())
            {
                Debug.Log("game over");
            }
        }
    }
}

