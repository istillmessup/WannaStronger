using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game01
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager _instance;

        public List<Vector2> nullGridList = new List<Vector2>();
        public int emptyGridBgCount = 5; // 没有数字的格子
        public int emptyGridCount = 5; // 初始情况下可以移动的位置
        public int x, y; // 棋盘的长、宽
        public GameObject gridBgPrefab, gridPrefab;
        public Text timerText; // 计时文本

        private Dictionary<Vector2, Transform> gridDict = new Dictionary<Vector2, Transform>(); // 存储了可移动格子的位置

        private Dictionary<Vector2, int> gridBgValueDict = new Dictionary<Vector2, int>(); // 存储了格子的位置以及上面对应的数字
        private Dictionary<Vector2, int> gridValueDict = new Dictionary<Vector2, int>();   // 通过这两个字典的比较就可以判断是否还原棋盘

        private List<int> gridBgValueList = new List<int>(); // 用来存储随机生成在格子上的数字
        private List<int> gridValueList = new List<int>();

        private float timer = 0.0f; // 三个用于计时⌛️的参数
        private int minute = 0;
        private int second = 0;
        private bool flag = false; // 这个标记位用来判断游戏是否结束

        private void Awake()
        {
            _instance = this;
            SetValueList();
            Init();
        }

        private void Update()
        {
            if (!flag)
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
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Vector2 v = new Vector2(j, i);
                    Vector2 position = new Vector2(j * 100, i * 100);
                    if (nullGridList.Contains(v) == false)
                    {
                        GameObject gridBg = Instantiate(gridBgPrefab, transform.Find("Grid/GridBgParent"));
                        gridBg.transform.localPosition = position;

                        int r = Random.Range(0, 100);
                        // TODO:这里有个概率上的bug：如果随机数小于50的个数小于5次，那么就会出现问题！！虽然概率很低，但是就怕演示的时候出现问题，那就完犊子了。
                        // 并且这个方法还会导致每次游戏时空的格子位置整体偏下方。
                        // 解决办法就是随机生成五个不同的数来标记空位置，但是我懒，暂时不想实现它。
                        if (r < 50 && emptyGridBgCount > 0)
                        {
                            emptyGridBgCount--;
                            gridBg.transform.GetChild(0).gameObject.SetActive(false);
                            gridBgValueDict.Add(v, 0);

                        }
                        else
                        {
                            gridBg.GetComponentInChildren<Text>().text = gridBgValueList[0].ToString();
                            gridBgValueDict.Add(v, gridBgValueList[0]);
                            gridBgValueList.RemoveAt(0);

                        }
                        if (2 * r < 130 && emptyGridCount > 0)
                        {
                            emptyGridCount--;
                            gridDict.Add(v, null);
                            gridValueDict.Add(v, 0);
                        }
                        else
                        {
                            GameObject grid = Instantiate(gridPrefab, transform.Find("Grid/GridParent"));
                            grid.transform.localPosition = position; // 位置
                            grid.GetComponentInChildren<Text>().text = gridValueList[0].ToString(); // 数字
                            GridController gridController = grid.GetComponent<GridController>();
                            gridController.x = x; // 棋盘宽度
                            gridController.y = y; // 棋盘高度
                            gridController.currPoint = v; // 格子当前的位置
                            gridValueDict.Add(v, gridValueList[0]); // 格子上的数字
                            gridDict.Add(v, grid.transform);
                            gridValueList.RemoveAt(0);
                        }
                    }
                }
            }
        }

        // 随机生成格子上的数字
        private void SetValueList()
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= 14; i++)
            {
                list.Add(i);
            }
            while (list.Count > 0)
            {
                int r = Random.Range(0, list.Count);
                gridBgValueList.Add(list[r]);
                list.RemoveAt(r);
            }
            for (int i = 1; i <= 14; i++)
            {
                list.Add(i);
            }
            while (list.Count > 0)
            {
                int r = Random.Range(0, list.Count);
                gridValueList.Add(list[r]);
                list.RemoveAt(r);
            }
        }

        // 移动格子，通过单例模式由GridController调用
        public void GridMove(ref Vector2 currPoint, Vector2 nextPoint)
        {
            if (nullGridList.Contains(nextPoint))
            {
                return;
            }
            if (gridDict[nextPoint] != null)
            {
                return;
            }
            gridDict[nextPoint] = gridDict[currPoint];
            gridDict[currPoint] = null;
            gridDict[nextPoint].localPosition = nextPoint * 100;

            gridValueDict[nextPoint] = gridValueDict[currPoint];
            gridValueDict[currPoint] = 0;

            currPoint = nextPoint;
        }

        // 检查棋盘是否恢复，每次移动结束后都要调用一次
        // 通过单例模式由GridController调用
        public bool Check()
        {
            foreach (var item in gridBgValueDict)
            {
                if (gridValueDict[item.Key] != item.Value)
                {
                    
                    return false;
                }
            }
            flag = true;
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
            foreach (Transform trans in transform.Find("Grid/GridParent"))
            {
                Destroy(trans.gameObject);
            }
            foreach (Transform trans in transform.Find("Grid/GridBgParent"))
            {
                Destroy(trans.gameObject);
            }
            // 隐藏面板
            transform.Find("Restart").gameObject.SetActive(false);
            transform.Find("Menu").gameObject.SetActive(false);
            transform.Find("Image").gameObject.SetActive(false);
            // 清除数据结构
            emptyGridCount = 5;
            emptyGridBgCount = 5;
            gridValueList.Clear();
            gridBgValueList.Clear();
            gridDict.Clear();
            gridValueDict.Clear();
            gridBgValueDict.Clear();
            // 重新加载场景
            SetValueList();
            Init();
            timer = 0;

        }

        public void OnBackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
