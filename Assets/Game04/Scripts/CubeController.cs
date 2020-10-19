using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game04
{
    public class CubeController : MonoBehaviour
    {
        // 鼠标第一次点击时间、第二次点击时间
        private float start, end = 0;
        // 判断该点是否已经被设置过
        private bool flag = false;
        // 立方体的三维坐标
        public Vector3 position;
        // key:立方体所在的面 value:在这个面上对应的二维坐标
        public SurfaceTypeVector2Dictionary surfaceDict = new SurfaceTypeVector2Dictionary();
        private void OnMouseDown()
        {
            start = Time.realtimeSinceStartup;
            if (start - end < .3f)
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
                    MapManager._instance.SetNextPoint(surfaceDict, position);
                }
                // 设置沿途点
                else if (UIManager._instance.flag == 1)
                {
                    if (flag == true)
                    {
                        //TODO:输出提示信息 这个点已经被设置过了
                        HintBox._instance.ShowMessage("");
                        return;
                    }
                    if (MapManager._instance.nextPointList.Contains(position) == false)
                    {
                        //TODO:输出提示信息 按照马走日的规范
                        HintBox._instance.ShowMessage("好马不吃回头草");
                        return;
                    }
                    Material material = new Material(GetComponent<MeshRenderer>().material);
                    material.color = Color.white;
                    GetComponent<MeshRenderer>().material = material;
                    flag = true;
                    MapManager._instance.SetNextPoint(surfaceDict, position);
                }
            }
            end = start;
        }
    }

    public enum SurfaceType
    {
        xy,
        yz,
        xz
    }
}
