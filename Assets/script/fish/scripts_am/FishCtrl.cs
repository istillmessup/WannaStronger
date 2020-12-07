using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FishCtrl : MonoBehaviour
{
    public FishInfo info { get; set; }
   
    void Start()
    {
    }
    /// <summary>
    /// 设置鱼
    /// </summary>
    /// <param name="_info"></param>
    public void SetFish(FishInfo _info)
    {
        info = _info;//当前食物对象

    }
   
}
