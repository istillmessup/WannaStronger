using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game04
{
    public class CubeController : MonoBehaviour
    {
        // 鼠标第一次点击时间、第二次点击时间
        private float start, end = 0;
        // 出题：判断该点是否已经被设置过 答题：判断该点是否可以点击
        [HideInInspector]
        public bool flag = false;
        // 立方体的三维坐标
        [HideInInspector]
        public Vector3 position;
        // key:立方体所在的面 value:在这个面上对应的二维坐标
        [HideInInspector]
        public SurfaceTypeVector2Dictionary surfaceDict = new SurfaceTypeVector2Dictionary();
       
        private void OnMouseDown()
        {
            start = Time.realtimeSinceStartup;
            if (start - end < .2f)
            {
                // 设置起点
                if (UIManager._instance.flag == 0)
                {
                    UIManager._instance.flag = 1;
                    // 修改立方体的材质球时不能直接修改，否则所有的立方体都会改变
                    // 应该先对被点击的立方体的材质球进行拷贝，修改复制出来的材质球，然后再赋值回去即可
                    Material material = new Material(GetComponent<MeshRenderer>().material);
                    material.color = Color.red;
                    GetComponent<MeshRenderer>().material = material;
                    flag = true;
                    MapManager._instance.SetNextPoint(surfaceDict, position, this.gameObject, true);
                }
                // 设置沿途点
                else if (UIManager._instance.flag == 1)
                {
                    if (flag == true)
                    {
                        HintBox._instance.ShowMessage("好马不吃回头草");
                        return;
                    }
                    if (MapManager._instance.nextPointList.Contains(position) == false)
                    {
                        HintBox._instance.ShowMessage("想想马怎么走");
                        return;
                    }
                    Material material = new Material(GetComponent<MeshRenderer>().material);
                    material.color = Color.white;
                    GetComponent<MeshRenderer>().material = material;
                    flag = true;
                    MapManager._instance.SetNextPoint(surfaceDict, position, this.gameObject, true);
                }
                else if (UIManager._instance.flag == -1)
                {
                    if (flag == false || !MapManager._instance.nextPointList.Contains(position))
                    {
                        HintBox._instance.ShowMessage("走错了，笨比");
                        return;
                    }
                    flag = false;
                    Material material = new Material(GetComponent<MeshRenderer>().material);
                    material.color = Color.magenta;
                    GetComponent<MeshRenderer>().material = material;
                    MapManager._instance.SetNextPoint(surfaceDict, position, this.gameObject, true);
                    Check();
                }
            }
            end = start;
        }

        private void Check()
        {
            MapManager._instance.Check();
        }
    }

    public enum SurfaceType
    {
        xy,
        yz,
        xz
    }
}
