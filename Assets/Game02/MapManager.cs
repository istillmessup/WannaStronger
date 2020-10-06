using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game02
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager _instance;

        public GameObject gridPrefab;
        public GameObject rotatorPrefab;
        public Color[] color;
        public List<Color> gridColorList = new List<Color>();

        private List<Color> targetColorList = new List<Color>();
        private Transform gridParent;
        private Dictionary<Vector2, Transform> rotateDict = new Dictionary<Vector2, Transform>();
        private int flag = 1;

        private void Awake()
        {
            _instance = this;
            gridParent = GameObject.Find("GridParent").transform;
            SetTargetList();
            SetColorList();
            Init();
        }

        // 这个方法里面有大量的重复代码，可以对此进行封装
        // 但是切记在修改前备份一下，避免修改后出现什么奇怪的bug，(你肯定会遇到bug)
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
                            GameObject grid = Instantiate(gridPrefab, gridParent);
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
                            rotateDict.Add(new Vector2(j, i), grid.transform);
                            flag *= -1;
                        }
                    }
                    // 第二行和第五行
                    else if (i == 1 || i == 4)
                    {
                        if (j >= 1 && j <= 9)
                        {
                            GameObject grid = Instantiate(gridPrefab, gridParent);
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
                            rotateDict.Add(new Vector2(j, i), grid.transform);
                            flag *= -1;
                        }
                    }
                    // 第三行和第四行
                    else
                    {
                        GameObject grid = Instantiate(gridPrefab, gridParent);
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
                        rotateDict.Add(new Vector2(j, i), grid.transform);
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
                        GameObject rotator = Instantiate(rotatorPrefab, gridParent);
                        rotator.transform.localPosition = new Vector3(j * 70, i * 120 + 52.5f, 0);
                        rotator.GetComponent<GridController>().x = j;
                        rotator.GetComponent<GridController>().y = i;
                    }
                }
                else if (i == 1 || i == 3)
                {
                    for (int j = 2; j <= 8; j += 2)
                    {
                        GameObject rotator = Instantiate(rotatorPrefab, gridParent);
                        rotator.transform.localPosition = new Vector3(j * 70, i * 120 + 52.5f, 0);
                        rotator.GetComponent<GridController>().x = j;
                        rotator.GetComponent<GridController>().y = i;
                    }
                }
                else
                {
                    for (int j = 1; j <= 9; j += 2)
                    {
                        GameObject rotator = Instantiate(rotatorPrefab, gridParent);
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

        private void SetTargetList()
        {
            for (int i = 0; i < 6; i++)
            {
                targetColorList.Add(color[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                int r1 = Random.Range(0, targetColorList.Count);
                int r2 = Random.Range(0, targetColorList.Count);
                Color temp = targetColorList[r1];
                targetColorList[r1] = targetColorList[r2];
                targetColorList[r2] = temp;
            }
            transform.Find("Target").GetChild(0).GetComponent<Image>().color = targetColorList[0];
            transform.Find("Target").GetChild(1).GetComponent<Image>().color = targetColorList[1];
            transform.Find("Target").GetChild(2).GetComponent<Image>().color = targetColorList[2];
            transform.Find("Target").GetChild(3).GetComponent<Image>().color = targetColorList[3];
            transform.Find("Target").GetChild(4).GetComponent<Image>().color = targetColorList[4];
            transform.Find("Target").GetChild(5).GetComponent<Image>().color = targetColorList[5];
        }

        public void Rotate(int x, int y)
        {
            Vector2 position = rotateDict[new Vector2(x, y)].localPosition;
            Vector3 angle = rotateDict[new Vector2(x, y)].localEulerAngles;

            rotateDict[new Vector2(x, y)].localPosition = rotateDict[new Vector2(x - 1, y)].localPosition;
            rotateDict[new Vector2(x, y)].localEulerAngles = rotateDict[new Vector2(x - 1, y)].localEulerAngles;

            rotateDict[new Vector2(x - 1, y)].localPosition = rotateDict[new Vector2(x - 1, y + 1)].localPosition;
            rotateDict[new Vector2(x - 1, y)].localEulerAngles = rotateDict[new Vector2(x - 1, y + 1)].localEulerAngles;

            rotateDict[new Vector2(x - 1, y + 1)].localPosition = rotateDict[new Vector2(x, y + 1)].localPosition;
            rotateDict[new Vector2(x - 1, y + 1)].localEulerAngles = rotateDict[new Vector2(x, y + 1)].localEulerAngles;

            rotateDict[new Vector2(x, y + 1)].localPosition = rotateDict[new Vector2(x + 1, y + 1)].localPosition;
            rotateDict[new Vector2(x, y + 1)].localEulerAngles = rotateDict[new Vector2(x + 1, y + 1)].localEulerAngles;

            rotateDict[new Vector2(x + 1, y + 1)].localPosition = rotateDict[new Vector2(x + 1, y)].localPosition;
            rotateDict[new Vector2(x + 1, y + 1)].localEulerAngles = rotateDict[new Vector2(x + 1, y)].localEulerAngles;

            rotateDict[new Vector2(x + 1, y)].localPosition = position;
            rotateDict[new Vector2(x + 1, y)].localEulerAngles = angle;

            Transform temp = rotateDict[new Vector2(x, y)];
            rotateDict[new Vector2(x, y)] = rotateDict[new Vector2(x + 1, y)];
            rotateDict[new Vector2(x + 1, y)] = rotateDict[new Vector2(x + 1, y + 1)];
            rotateDict[new Vector2(x + 1, y + 1)] = rotateDict[new Vector2(x, y + 1)];
            rotateDict[new Vector2(x, y + 1)] = rotateDict[new Vector2(x - 1, y + 1)];
            rotateDict[new Vector2(x - 1, y + 1)] = rotateDict[new Vector2(x - 1, y)];
            rotateDict[new Vector2(x - 1, y)] = temp;
        }

        public bool Check()
        {
            return (CheckA(2, 1, 0) && CheckA(8, 1, 2) && CheckA(5, 4, 4) && CheckB(5, 0, 1) && CheckB(2, 3, 3) && CheckB(8, 3, 5));
        }

        private bool CheckA(int x, int y, int index)
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(rotateDict[new Vector2(x, y - 1)].GetComponent<Image>().color);
            
            colorList.Add(rotateDict[new Vector2(x - 1, y)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x, y)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x + 1, y)].GetComponent<Image>().color);
            
            colorList.Add(rotateDict[new Vector2(x - 2, y + 1)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x - 1, y + 1)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x, y + 1)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x + 1, y + 1)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x + 2, y + 1)].GetComponent<Image>().color);

            foreach (Color item in colorList)
            {
                if (item != targetColorList[index])
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckB(int x, int y, int index)
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(rotateDict[new Vector2(x, y + 2)].GetComponent<Image>().color);
            
            colorList.Add(rotateDict[new Vector2(x - 1, y + 1)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x, y + 1)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x + 1, y + 1)].GetComponent<Image>().color);
            
            colorList.Add(rotateDict[new Vector2(x - 2, y)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x - 1, y)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x, y)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x + 1, y)].GetComponent<Image>().color);
            colorList.Add(rotateDict[new Vector2(x + 2, y)].GetComponent<Image>().color);

            foreach (Color item in colorList)
            {
                if (item != targetColorList[index])
                {
                    return false;
                }
            }
            return true;
        }

        public void OnRestart()
        {
            gridColorList.Clear();
            rotateDict.Clear();
            targetColorList.Clear();

            SetTargetList();
            SetColorList();
            Init();
        }
    }
}
