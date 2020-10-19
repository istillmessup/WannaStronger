using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game04
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager _instance;
        public GameObject cubePrefab;
        // 下一步可以被点击的立方体
        public List<Vector3> nextPointList = new List<Vector3>();

        private void Awake()
        {
            _instance = this;
            Init();
        }

        private void Update()
        {
            // 鼠标控制立方体旋转
            if (Input.GetMouseButton(0))
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");
                transform.Rotate(new Vector3(y, -x, 0) * 6, Space.World);
            }
        }

        private void Init()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (i >= 1 && i <= 6 && j >= 1 && j <= 6 && k >= 1 && k <=6)
                        {
                            continue;
                        }
                        GameObject cube = Instantiate(cubePrefab, transform.GetChild(0));
                        cube.transform.localPosition = new Vector3(i, j, k);
                        CubeController c = cube.AddComponent<CubeController>();
                        c.position = new Vector3(i, j, k);
                        // 给surfaceDict赋值，可以封装
                        if (k == 0 || k == 7)
                        {
                            Vector2 xy = new Vector2(i, j);
                            if (c.surfaceDict.ContainsKey(SurfaceType.xy) == false)
                            {
                                c.surfaceDict.Add(SurfaceType.xy, xy);
                            }
                        }
                        if (j == 0 || j == 7)
                        {
                            Vector2 xz = new Vector2(i, k);
                            if (c.surfaceDict.ContainsKey(SurfaceType.xz) == false)
                            {
                                c.surfaceDict.Add(SurfaceType.xz, xz);
                            }
                        }
                        if (i == 0 || i == 7)
                        {
                            Vector2 yz = new Vector2(j, k);
                            if (c.surfaceDict.ContainsKey(SurfaceType.yz) == false)
                            {
                                c.surfaceDict.Add(SurfaceType.yz, yz);
                            }
                        }
                    }
                }
            }
        }

        // 设置下一步可以被点击(可以经过)的立方体
        public void SetNextPoint(SurfaceTypeVector2Dictionary dict, Vector3 position)
        {
            nextPointList.Clear();
            foreach (var item in dict)
            {
                switch (item.Key)
                {
                    case SurfaceType.xy:
                        nextPointList.Add(new Vector3(item.Value.x + 2, item.Value.y + 1, position.z));
                        nextPointList.Add(new Vector3(item.Value.x + 2, item.Value.y - 1, position.z));
                        nextPointList.Add(new Vector3(item.Value.x - 2, item.Value.y + 1, position.z));
                        nextPointList.Add(new Vector3(item.Value.x - 2, item.Value.y - 1, position.z));
                        nextPointList.Add(new Vector3(item.Value.x + 1, item.Value.y + 2, position.z));
                        nextPointList.Add(new Vector3(item.Value.x + 1, item.Value.y - 2, position.z));
                        nextPointList.Add(new Vector3(item.Value.x - 1, item.Value.y + 2, position.z));
                        nextPointList.Add(new Vector3(item.Value.x - 1, item.Value.y - 2, position.z));
                        break;
                    case SurfaceType.yz:
                        nextPointList.Add(new Vector3(position.x, item.Value.x + 2, item.Value.y + 1));
                        nextPointList.Add(new Vector3(position.x, item.Value.x + 2, item.Value.y - 1));
                        nextPointList.Add(new Vector3(position.x, item.Value.x - 2, item.Value.y + 1));
                        nextPointList.Add(new Vector3(position.x, item.Value.x - 2, item.Value.y - 1));
                        nextPointList.Add(new Vector3(position.x, item.Value.x + 1, item.Value.y + 2));
                        nextPointList.Add(new Vector3(position.x, item.Value.x + 1, item.Value.y - 2));
                        nextPointList.Add(new Vector3(position.x, item.Value.x - 1, item.Value.y + 2));
                        nextPointList.Add(new Vector3(position.x, item.Value.x - 1, item.Value.y - 2));
                        break;
                    case SurfaceType.xz:
                        nextPointList.Add(new Vector3(item.Value.x + 2, position.y, item.Value.y + 1));
                        nextPointList.Add(new Vector3(item.Value.x + 2, position.y, item.Value.y - 1));
                        nextPointList.Add(new Vector3(item.Value.x - 2, position.y, item.Value.y + 1));
                        nextPointList.Add(new Vector3(item.Value.x - 2, position.y, item.Value.y - 1));
                        nextPointList.Add(new Vector3(item.Value.x + 1, position.y, item.Value.y + 2));
                        nextPointList.Add(new Vector3(item.Value.x + 1, position.y, item.Value.y - 2));
                        nextPointList.Add(new Vector3(item.Value.x - 1, position.y, item.Value.y + 2));
                        nextPointList.Add(new Vector3(item.Value.x - 1, position.y, item.Value.y - 2));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

