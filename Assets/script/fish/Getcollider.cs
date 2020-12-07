using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//鱼与边框发生碰撞
public class Getcollider : MonoBehaviour
{
    Transform fishGondManage;
    public bool Trigger;
    public int showi;
    public int all;
    // Start is called before the first frame update
    void Start()
    {
        fishGondManage = GameObject.Find("fishGondManage").transform;
        Trigger = false;
    }


    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Trigger = true;
    //    all = fishGondManage.childCount;
    //    int temp = Random.Range(0, fishGondManage.childCount);//在第二步的随机点中随机选一个出来
    //    showi = temp;
    //    while (temp == collision.gameObject.GetComponent<Move>().index)//这个随机点和前一个随机点不能相同，如果相同的话，就会直接出边界了。
    //    {
    //        temp = Random.Range(0, fishGondManage.childCount);
    //    }
    //    collision.gameObject.GetComponent<Move>().dir = fishGondManage.GetChild(temp).position - collision.transform.position;//给游戏物体一个到随机点的方向，这个方向就是它接下来的运动方向。
    //    collision.gameObject.GetComponent<Move>().index = temp;//记录当前随机点
    //}


}
