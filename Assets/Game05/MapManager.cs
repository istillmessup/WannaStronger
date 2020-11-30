using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game05
{
    public class MapManager : MonoBehaviour
    {
        private Dictionary<Vector2, GridController> gridDict = new Dictionary<Vector2, GridController>();
        private Dictionary<Vector2, GridController> gridTargetDict = new Dictionary<Vector2, GridController>();
        private float timer = 0.0f; // 三个用于计时⌛️的参数
        private int minute = 0;
        private int second = 0;

        public static MapManager _instance;
        public GameObject gridPrefab;
        public Text timerText; // 计时文本

        private void Awake()
        {
            _instance = this;
            Init();
            SetTarget();
        }

        private void Update()
        {
            //TODO:计时
            timer += Time.deltaTime;
            minute = (int)(timer / 60);
            second = (int)timer - minute * 60;
            timerText.text = string.Format("[{0:D2} : {1:D2}]", minute, second);
        }

        private void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // 初始图形
                    GameObject grid1 = Instantiate(gridPrefab, transform.Find("GridParent"));
                    grid1.transform.localPosition = new Vector2(i * 100, j * 100);
                    gridDict.Add(new Vector2(i, j), grid1.GetComponent<GridController>());
                    grid1.GetComponent<GridController>().x = i;
                    grid1.GetComponent<GridController>().y = j;
                    // 目标图形
                    GameObject grid2 = Instantiate(gridPrefab, transform.Find("TargetParent"));
                    grid2.transform.localPosition = new Vector2(i * 100, j * 100);
                    grid2.GetComponent<Button>().enabled = false;
                    
                    gridTargetDict.Add(new Vector2(i, j), grid2.GetComponent<GridController>());
                }
            }
        }

        private void SetTarget()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int r = Random.Range(0, 10);
                    if (r < 4)
                    {
                        if (x != 0)
                        {
                            gridTargetDict[new Vector2(x - 1, y)].OnClick();
                        }
                        if (x != 7)
                        {
                            gridTargetDict[new Vector2(x + 1, y)].OnClick();
                        }
                        if (y != 0)
                        {
                            gridTargetDict[new Vector2(x, y - 1)].OnClick();
                        }
                        if (y != 7)
                        {
                            gridTargetDict[new Vector2(x, y + 1)].OnClick();
                        }
                        gridTargetDict[new Vector2(x, y)].OnClick();
                        // Debug.Log(new Vector2(x, y));
                    }
                }
            }
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 7; x >= 0; x--)
                {
                    int r = Random.Range(0, 10);
                    if (r < 4)
                    {
                        if (x != 0)
                        {
                            gridTargetDict[new Vector2(x - 1, y)].OnClick();
                        }
                        if (x != 7)
                        {
                            gridTargetDict[new Vector2(x + 1, y)].OnClick();
                        }
                        if (y != 0)
                        {
                            gridTargetDict[new Vector2(x, y - 1)].OnClick();
                        }
                        if (y != 7)
                        {
                            gridTargetDict[new Vector2(x, y + 1)].OnClick();
                        }
                        gridTargetDict[new Vector2(x, y)].OnClick();
                        // Debug.Log(new Vector2(x, y));
                    }
                }
            }
        }

        public void ChangeColor(int x, int y)
        {
            if (x != 0)
            {
                gridDict[new Vector2(x - 1, y)].OnClick(); 
            }
            if (x != 7)
            {
                gridDict[new Vector2(x + 1, y)].OnClick();
            }
            if (y != 0)
            {
                gridDict[new Vector2(x, y - 1)].OnClick();
            }
            if (y != 7)
            {
                gridDict[new Vector2(x, y + 1)].OnClick();
            }
            gridDict[new Vector2(x, y)].OnClick();
            
        }

        public bool Check()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gridDict[new Vector2(i, j)].isWhite != gridTargetDict[new Vector2(i, j)].isWhite)
                    {
                        return false;
                    }
                }
            }
            return true;
        }    
    }
}

