using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game02
{
    public class MapManager : MonoBehaviour
    {
        public GameObject gridPrefab;
        public GameObject rotatorPrefab;
        public Color[] color;
        public List<Color> gridColorList = new List<Color>();

        private int flag = 1;

        private void Awake()
        {
            SetColorList();
            Init();
        }

        // 这个方法里面有大量的重复代码，可以对此进行封装
        // 但是切记在修改前备份一下，避免修改后出现什么奇怪的bug
        // 不要为我为什么不做，因为我懒
        private void Init()
        {
            for (int i = 0; i < 6; i++)
            {
                flag = 1;
                for (int j = 0; j < 11; j++)
                {
                    // 第一行和第六行
                    if (i == 0 || i == 5)
                    {
                        if (j >= 2 && j <= 8)
                        {
                            GameObject grid = Instantiate(gridPrefab, transform);
                            grid.GetComponent<Image>().color = gridColorList[0];
                            gridColorList.RemoveAt(0);
                            if (i == 0)
                            {
                                if (flag > 0)
                                {
                                    grid.transform.localPosition = new Vector3(j * 70, i * 120, 0);
                                }
                                else
                                {
                                    grid.transform.localEulerAngles = new Vector3(0, 0, 180);
                                    grid.transform.localPosition = new Vector3(j * 70, i * 120 - 15, 0);
                                }    
                            }
                            if (i == 5)
                            {
                                if (flag > 0)
                                {
                                    grid.transform.localEulerAngles = new Vector3(0, 0, 180);
                                    grid.transform.localPosition = new Vector3(j * 70, i * 120 - 15, 0);
                                }
                                else
                                {
                                    grid.transform.localPosition = new Vector3(j * 70, i * 120, 0);
                                }               
                            }
                            flag *= -1;
                        }
                    }
                    // 第二行和第五行
                    else if (i == 1 || i == 4)
                    {
                        if (j >= 1 && j <= 9)
                        {
                            GameObject grid = Instantiate(gridPrefab, transform);
                            grid.GetComponent<Image>().color = gridColorList[0];
                            gridColorList.RemoveAt(0);
                            if (i == 1)
                            {
                                if (flag > 0)
                                {
                                    grid.transform.localPosition = new Vector3(j * 70, i * 120, 0);
                                }
                                else
                                {
                                    grid.transform.localEulerAngles = new Vector3(0, 0, 180);
                                    grid.transform.localPosition = new Vector3(j * 70, i * 120 - 15, 0);
                                }
                            }
                            if (i == 4)
                            {
                                if (flag > 0)
                                {
                                    grid.transform.localEulerAngles = new Vector3(0, 0, 180);
                                    grid.transform.localPosition = new Vector3(j * 70, i * 120 - 15, 0);
                                }
                                else
                                {
                                    grid.transform.localPosition = new Vector3(j * 70, i * 120, 0);
                                }
                            }
                            flag *= -1;
                        }
                    }
                    // 第三行和第四行
                    else
                    {
                        GameObject grid = Instantiate(gridPrefab, transform);
                        grid.GetComponent<Image>().color = gridColorList[0];
                        gridColorList.RemoveAt(0);
                        if (i == 2)
                        {
                            if (flag > 0)
                            {
                                grid.transform.localPosition = new Vector3(j * 70, i * 120, 0);
                            }
                            else
                            {
                                grid.transform.localEulerAngles = new Vector3(0, 0, 180);
                                grid.transform.localPosition = new Vector3(j * 70, i * 120 - 15, 0);
                            }
                        }
                        if (i == 3)
                        {
                            if (flag > 0)
                            {
                                grid.transform.localEulerAngles = new Vector3(0, 0, 180);
                                grid.transform.localPosition = new Vector3(j * 70, i * 120 - 15, 0);
                            }
                            else
                            {
                                grid.transform.localPosition = new Vector3(j * 70, i * 120, 0);
                            }
                        }
                        flag *= -1;
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (i == 0 || i == 4)
                {
                    for (int j = 3; j <= 7; j += 2)
                    {
                        GameObject rotator = Instantiate(rotatorPrefab, transform);
                        rotator.transform.localPosition = new Vector3(j * 70, i * 120 + 52.5f, 0);
                        rotator.GetComponent<GridController>().x = j;
                        rotator.GetComponent<GridController>().y = i;
                    }
                }
                else if (i == 1 || i == 3)
                {
                    for (int j = 2; j <= 8; j += 2)
                    {
                        GameObject rotator = Instantiate(rotatorPrefab, transform);
                        rotator.transform.localPosition = new Vector3(j * 70, i * 120 + 52.5f, 0);
                        rotator.GetComponent<GridController>().x = j;
                        rotator.GetComponent<GridController>().y = i;
                    }
                }
                else
                {
                    for (int j = 1; j <= 9; j += 2)
                    {
                        GameObject rotator = Instantiate(rotatorPrefab, transform);
                        rotator.transform.localPosition = new Vector3(j * 70, i * 120 + 52.5f, 0);
                        rotator.GetComponent<GridController>().x = j;
                        rotator.GetComponent<GridController>().y = i;
                    }
                }
            }
        }

        private void SetColorList()
        {
            List<Color> list = new List<Color>();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    list.Add(color[i]);
                }
            }
            while (list.Count > 0)
            {
                int index = Random.Range(0, list.Count);
                gridColorList.Add(list[index]);
                list.RemoveAt(index);
            }
        }

        public void Rotate(int x, int y)
        {

        }
    }
}
