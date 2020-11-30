using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Game03
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager _instance;
        public static bool flag = false;

        public GameObject gridPrefab;
        public Color[] color;
        public Button button1, button2, button3;
        public Text timerText; // 计时文本

        private Dictionary<Vector2, Transform> gridDict = new Dictionary<Vector2, Transform>();
        private List<Color> gridColorList = new List<Color>();
        private Transform gridParent;
        private float timer = 0.0f; // 三个用于计时⌛️的参数
        private int minute = 0;
        private int second = 0;
        private bool flag1 = false; // 这个标记位用来判断游戏是否结束

        private void Awake()
        {
            _instance = this;
            flag = false;
            gridParent = GameObject.Find("GridParent").transform;
            // 开始按钮
            button1.onClick.AddListener(() =>
            {
                flag = true;
                button1.gameObject.SetActive(false);
                button2.gameObject.SetActive(true);
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        gridDict[new Vector2(i, j)].GetComponent<Image>().color = Color.gray;
                    }
                }
            });
            // 显示按钮
            button2.onClick.AddListener(() =>
            {
                flag = false;
                button2.gameObject.SetActive(false);
                button3.gameObject.SetActive(true);
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        gridDict[new Vector2(i, j)].GetComponent<Image>().color = gridDict[new Vector2(i, j)].GetComponent<GridController>().color;
                    }
                }
            });
            // 隐藏按钮
            button3.onClick.AddListener(() =>
            {
                flag = true;
                button3.gameObject.SetActive(false);
                button2.gameObject.SetActive(true);
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        gridDict[new Vector2(i, j)].GetComponent<Image>().color = Color.gray;
                    }
                }
            });
            SetColorList();
            Init();
        }

        private void Update()
        {
            //TODO:计时
            if (!flag1)
            {
                timer += Time.deltaTime;
                minute = (int)(timer / 60);
                second = (int)timer - minute * 60;
                timerText.text = string.Format("[{0:D2} : {1:D2}]", minute, second);
            }
        }

        private void Init()
        {
            flag = false;
            flag1 = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    GameObject grid = Instantiate(gridPrefab, gridParent);
                    grid.transform.localPosition = new Vector2(i * 100, j * 100);
                    grid.GetComponent<GridController>().x = i;
                    grid.GetComponent<GridController>().y = j;
                    grid.GetComponent<GridController>().color = gridColorList[0];
                    grid.GetComponent<Image>().color = gridColorList[0];
                    gridColorList.RemoveAt(0);

                    gridDict.Add(new Vector2(i, j), grid.transform);
                }
            }
        }
        private void SetColorList()
        {
            List<Color> list = new List<Color>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    list.Add(color[i]);
                }
            }

            while (list.Count > 0)
            {
                int r = Random.Range(0, list.Count);
                gridColorList.Add(list[r]);
                list.RemoveAt(r);
            }
        }

        public void GridLeftMove(int y)
        {
            for (int i = 4; i >= 0; i--)
            {
                if (i != 0)
                {
                    gridDict[new Vector2(i, y)].GetComponent<GridController>().x--;
                    gridDict[new Vector2(i, y)].DOLocalMoveX((i - 1) * 100, .3f);
                }
                else
                {
                    gridDict[new Vector2(i, y)].GetComponent<GridController>().x = 4;
                    GameObject go = gridDict[new Vector2(i, y)].gameObject;
                    go.SetActive(false);
                    gridDict[new Vector2(i, y)].DOLocalMoveX(400, .2f).OnComplete(() =>
                    {
                        go.SetActive(true);
                    });
                }
            }
            Transform temp = gridDict[new Vector2(0, y)];
            for (int i = 0; i < 5; i++)
            {
                if (i != 4)
                {
                    gridDict[new Vector2(i, y)] = gridDict[new Vector2(i + 1, y)];
                }
                else
                {
                    gridDict[new Vector2(i, y)] = temp;
                }
            }
        }

        public void GridRightMove(int y)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i != 4)
                {
                    gridDict[new Vector2(i, y)].GetComponent<GridController>().x++;
                    gridDict[new Vector2(i, y)].DOLocalMoveX((i + 1) * 100, .3f);
                }
                else
                {
                    gridDict[new Vector2(i, y)].GetComponent<GridController>().x = 0;
                    GameObject go = gridDict[new Vector2(i, y)].gameObject;
                    go.SetActive(false);
                    gridDict[new Vector2(i, y)].DOLocalMoveX(0, .2f).OnComplete(() =>
                    {
                        go.SetActive(true);
                    });
                }
            }
            Transform temp = gridDict[new Vector2(4, y)];
            for (int i = 4; i >= 0; i--)
            {
                if (i != 0)
                {
                    gridDict[new Vector2(i, y)] = gridDict[new Vector2(i - 1, y)];
                }
                else
                {
                    gridDict[new Vector2(i, y)] = temp;
                }
            }
        }

        public void GridDownMove(int x)
        {
            for (int i = 4; i >= 0; i--)
            {
                if (i != 0)
                {
                    gridDict[new Vector2(x, i)].GetComponent<GridController>().y--;
                    gridDict[new Vector2(x, i)].DOLocalMoveY((i - 1) * 100, .3f);
                }
                else
                {
                    gridDict[new Vector2(x, i)].GetComponent<GridController>().y = 4;
                    GameObject go = gridDict[new Vector2(x, i)].gameObject;
                    go.SetActive(false);
                    gridDict[new Vector2(x, i)].DOLocalMoveY(400, .2f).OnComplete(() =>
                    {
                        go.SetActive(true);
                    });
                }
            }
            Transform temp = gridDict[new Vector2(x, 0)];
            for (int i = 0; i < 5; i++)
            {
                if (i != 4)
                {
                    gridDict[new Vector2(x, i)] = gridDict[new Vector2(x, i + 1)];
                }
                else
                {
                    gridDict[new Vector2(x, i)] = temp;
                }
            }
        }

        public void GridUpMove(int x)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i != 4)
                {
                    gridDict[new Vector2(x, i)].GetComponent<GridController>().y++;
                    gridDict[new Vector2(x, i)].DOLocalMoveY((i + 1) * 100, .3f);
                }
                else
                {
                    gridDict[new Vector2(x, i)].GetComponent<GridController>().y = 0;
                    GameObject go = gridDict[new Vector2(x, i)].gameObject;
                    go.SetActive(false);
                    gridDict[new Vector2(x, i)].DOLocalMoveY(0, .2f).OnComplete(() =>
                    {
                        go.SetActive(true);
                    });
                }
            }
            Transform temp = gridDict[new Vector2(x, 4)];
            for (int i = 4; i >= 0; i--)
            {
                if (i != 0)
                {
                    gridDict[new Vector2(x, i)] = gridDict[new Vector2(x, i - 1)];
                }
                else
                {
                    gridDict[new Vector2(x, i)] = temp;
                }
            }
        }

        public bool Check()
        {
            for (int i = 0; i < 5; i++)
            {
                Color[] temp = new Color[5];
                for (int j = 0; j < 5; j++)
                {
                    temp[j] = gridDict[new Vector2(j, i)].GetComponent<GridController>().color;
                }
                for (int j = 1; j < 5; j++)
                {
                    if (temp[0] != temp[j])
                    {
                        return false;
                    }
                }
            }
            flag1 = true;
            // 显示结算面板
            transform.Find("Restart").gameObject.SetActive(true);
            transform.Find("Menu").gameObject.SetActive(true);
            GameObject score = transform.Find("Image").gameObject;
            score.SetActive(true);
            score.GetComponentInChildren<Text>().text = string.Format("你的成绩\n[{0:D2} : {1:D2}]", minute, second);
            return true;
        }

        // TODO:重新开始、回主菜单两个函数
        public void OnRestart()
        {
            // 销毁场景中的prefab
            foreach (Transform trans in transform.Find("GridParent"))
            {
                Destroy(trans.gameObject);
            }
            // 隐藏面板
            transform.Find("Restart").gameObject.SetActive(false);
            transform.Find("Menu").gameObject.SetActive(false);
            transform.Find("Image").gameObject.SetActive(false);
            // 清除数据结构
            gridDict.Clear();
            gridColorList.Clear();
            // 重新加载场景
            SetColorList();
            Init();
            timer = 0;

        }

        public void OnBackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

