using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game06
{
    public class GridController : MonoBehaviour
    {
        public int direction; // 指针的方向
        
        public int x, y;
        public ColorType colorType;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                for (int i = 0; i < 7; i++)
                {
                    MapManager._instance.gridDict[new Vector2(0, i)].GetComponent<Button>().enabled = false;
                }
                Rotate();
            });
            
        }

        private void Rotate()
        {
            
            if (colorType == ColorType.Green)
            {
                transform.DOLocalRotate(new Vector3(0, 0, transform.localEulerAngles.z - 90), .3f).OnComplete(() =>
                {
                    direction++;
                    if (direction > 3)
                    {
                        direction = 0;
                    }
                    if (direction == 0) // 向上
                    {
                        if (y == 6)
                        {
                            return;
                        }
                        else
                        {
                            MapManager._instance.gridDict[new Vector2(x, y + 1)].GetComponent<GridController>().Rotate();
                        }
                    }
                    else if (direction == 1) // 向右
                    {
                        if (x == 6)
                        {
                            print("你赢了，真牛");
                            return;
                        }
                        else
                        {
                            MapManager._instance.gridDict[new Vector2(x + 1, y)].GetComponent<GridController>().Rotate();
                        }
                    }
                    else if (direction == 2) // 向下
                    {
                        if (y == 0)
                        {
                            return;
                        }
                        else
                        {
                            MapManager._instance.gridDict[new Vector2(x, y - 1)].GetComponent<GridController>().Rotate();
                        }
                    }
                    else if (direction == 3) // 向左
                    {
                        if (x == 0)
                        {
                            return;
                        }
                        else
                        {
                            MapManager._instance.gridDict[new Vector2(x - 1, y)].GetComponent<GridController>().Rotate();
                        }
                    }
                });
            }
            else if(colorType == ColorType.Purple)
            {
                transform.DOLocalRotate(new Vector3(0, 0, transform.localEulerAngles.z + 90), .3f).OnComplete(() =>
                {
                    direction++;
                    if (direction > 3)
                    {
                        direction = 0;
                    }
                    if (direction == 0) // 向上
                    {
                        MapManager._instance.gridDict[new Vector2(x, y + 1)].GetComponent<GridController>().Rotate();
                    }
                    else if (direction == 1) // 向左
                    {
                        if (x == 0)
                        {
                            return;
                        }
                        else
                        {
                            MapManager._instance.gridDict[new Vector2(x - 1, y)].GetComponent<GridController>().Rotate();
                        }
                    }
                    else if (direction == 2) // 向下
                    {
                        MapManager._instance.gridDict[new Vector2(x, y - 1)].GetComponent<GridController>().Rotate();
                    }
                    else if (direction == 3) // 向右
                    {
                        if (x == 6)
                        {
                            print("你赢了，真牛");
                            return;
                        }
                        else
                        {
                            MapManager._instance.gridDict[new Vector2(x + 1, y)].GetComponent<GridController>().Rotate();
                        }
                    }
                });
            }
            
        }

    }
}

