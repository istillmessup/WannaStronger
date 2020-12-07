using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 需要存储的数据类
/// 包括金币的数量、已经购买的鱼、音乐是否播放
/// 或许还可以存游戏的记录
/// </summary>
[Serializable]
public class GameData
{
   
    public int coinAmount;           //金币
    
    public List<FishInfo> fishInfos; //购买的鱼

    public bool mute;  //哑的，音乐是否播放
}

[Serializable]
public class FishInfo
{
    public int fishID;
}