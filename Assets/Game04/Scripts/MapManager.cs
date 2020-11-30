using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Game04
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager _instance;

        // 在设置路径时最后一次被点击的立方体
        private GameObject currCube;
        // 立方体的坐标以及对应的GameObject(这个数据结构是为了方便更改立方体的材质)
        private Dictionary<Vector3, GameObject> cubeDict = new Dictionary<Vector3, GameObject>();
        private float timer = 0.0f; // 三个用于计时⌛️的参数
        private int minute = 0;
        private int second = 0;

        [HideInInspector]
        public GameObject cubePrefab;
        // 下一步可以被点击的立方体
        [HideInInspector]
        public List<Vector3> nextPointList = new List<Vector3>();
        // 路径上所有点的顺序集合
        [HideInInspector]
        public List<Vector3> pointList = new List<Vector3>();
        public Text timerText; // 计时文本

        private void Awake()
        {
            _instance = this;
            Init();
            // 如果想要出题，请注释掉下面两个方法
            LoadLevelInfo();
            SetRoad();
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
                        cubeDict.Add(new Vector3(i, j, k), cube);
                        // 给surfaceDict赋值
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

        private void SetRoad()
        {
            for (int i = 0; i < Json.road.Count; i++)
            {
                GameObject cube = cubeDict[Json.road[i]];
                if (i == 0)
                {
                    SetNextPoint(cube.GetComponent<CubeController>().surfaceDict, Json.road[i], cube, true);
                    Material material = new Material(cube.GetComponent<MeshRenderer>().material);
                    material.color = Color.red;
                    cube.GetComponent<MeshRenderer>().material = material;
                }
                else if (i == Json.road.Count - 1)
                {
                    cube.GetComponent<CubeController>().flag = true;
                    Material material = new Material(cube.GetComponent<MeshRenderer>().material);
                    material.color = Color.green;
                    cube.GetComponent<MeshRenderer>().material = material;
                }
                else
                {
                    cube.GetComponent<CubeController>().flag = true;
                    Material material = new Material(cube.GetComponent<MeshRenderer>().material);
                    material.color = Color.white;
                    cube.GetComponent<MeshRenderer>().material = material;
                }
            }
        }
        
        private void LoadLevelInfo()
        {
            int length = Directory.GetFiles("LevelInfo/Game04").Length;
            int r = Random.Range(0, length);
            string file = "Map_" + r;
            Json.Load(file);
        }

        public void Check()
        {
            if (pointList.Count == Json.road.Count)
            {
                HintBox._instance.ShowMessage("牛逼");
                GameObject.Find("MapManager").gameObject.SetActive(false);
            }
        }

        // 设置下一步可以被点击(可以经过)的立方体
        public void SetNextPoint(SurfaceTypeVector2Dictionary dict, Vector3 position, GameObject obj, bool flag)
        {
            currCube = obj; // 更新最后一个被点击的立方体
            if (flag)
            {
                pointList.Add(position); // 将被点击的立方体加入路径列表中
            }
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

        // 设置终点
        public void SetEndPoint()
        {
            Material material = new Material(currCube.GetComponent<MeshRenderer>().material);
            material.color = Color.green;
            currCube.GetComponent<MeshRenderer>().material = material;
        }

        public void Undo()
        {
            if (pointList.Count == 1)
            {
                return;
            }
            Vector3 v = pointList[pointList.Count - 1];
            pointList.RemoveAt(pointList.Count - 1);
            GameObject temp = cubeDict[v];
            Material material = new Material(temp.GetComponent<MeshRenderer>().material);
            material.color = Color.white;
            temp.GetComponent<MeshRenderer>().material = material;
            temp.GetComponent<CubeController>().flag = true;

            Vector3 v1 = pointList[pointList.Count - 1];
            GameObject temp1 = cubeDict[v1];
            SetNextPoint(temp1.GetComponent<CubeController>().surfaceDict, v1, temp1, false);
        }
    }
}

