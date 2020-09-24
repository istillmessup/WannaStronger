using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public List<Vector2> nullGridList = new List<Vector2>();
    public int emptyGridBgCount = 5; // 没有数字的格子
    public int emptyGridCount = 5; // 初始情况下可以移动的位置
    public int x, y;
    public GameObject gridBgPrefab, gridPrefab;

    private List<int> gridBgValueList = new List<int>();
    private List<int> gridValueList = new List<int>();
    
    private void Awake()
    {
        SetValueList();
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Vector2 v = new Vector2(j, i);
                if (nullGridList.Contains(v) == false)
                {
                    GameObject gridBg = Instantiate(gridBgPrefab, transform.Find("GridBgParent"));
                    gridBg.transform.localPosition = new Vector2(j * 100, i * 100);
                    
                    int r = Random.Range(0, 100);
                    // 这里有个概率上的bug：如果随机数小于50的个数小于5次，那么就会出现问题！！虽然概率很低，但是就怕演示的时候出现问题，那就完犊子了。
                    // 并且这个方法还会导致每次游戏时空的格子位置整体偏下方。
                    // 解决办法就是随机生成五个不同的数来标记空位置，但是我懒，暂时不想实现它。
                    if (r < 50 && emptyGridBgCount > 0)
                    {
                        emptyGridBgCount--;
                        gridBg.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else
                    {
                        gridBg.GetComponentInChildren<Text>().text = gridBgValueList[0].ToString();
                        gridBgValueList.RemoveAt(0);
                    }
                    if (2 * r < 130 && emptyGridCount > 0)
                    {
                        emptyGridCount--;
                    }
                    else
                    {
                        GameObject grid = Instantiate(gridPrefab, transform.Find("GridParent"));
                        grid.transform.localPosition = new Vector2(j * 100, i * 100);
                        grid.GetComponentInChildren<Text>().text = gridValueList[0].ToString();
                        gridValueList.RemoveAt(0);
                    }
                }
            }
        }
    }

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
}
